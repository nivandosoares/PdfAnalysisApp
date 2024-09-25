using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PdfAnalysisApp.Data;
using PdfAnalysisApp.Models;


namespace PdfAnalysisApp.Controllers
{
    public class PdfController : Controller
    {
        private readonly AppDbContext _context;

        public PdfController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Upload() => View();

        [HttpPost]
    public async Task<IActionResult> Upload(IFormFile file)
    {
            if (file != null && file.Length > 0)
        {
            var fileName = Path.GetFileName(file.FileName);
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploads", fileName);

        using (var stream = new FileStream(filePath, FileMode.Create))
        {
            await file.CopyToAsync(stream);
        }

        var pdfFile = new PdfFile 
        { 
            FileName = fileName, 
            FilePath = filePath, 
            Feedback = string.Empty  // Define um valor padrão
        };

        _context.PdfFiles.Add(pdfFile);
        await _context.SaveChangesAsync();

          return RedirectToAction("Progress", new { id = pdfFile.Id });
        }

    return View();          
    }

        [HttpGet]
        public async Task<IActionResult> Progress(int id)
        {
            var pdfFile = await _context.PdfFiles.FindAsync(id);
            if (pdfFile == null) return NotFound();

            return View(pdfFile);
        }

        [HttpGet]
        public async Task<IActionResult> Analyze()
        {
            var pdfFiles = await _context.PdfFiles.ToListAsync();
            return View(pdfFiles);
        }

        [HttpPost]
        public async Task<IActionResult> Feedback(int id, string status, string feedback)
        {
            var pdfFile = await _context.PdfFiles.FindAsync(id);
            if (pdfFile == null) return NotFound();

            pdfFile.Status = status;
            pdfFile.Feedback = feedback;
            await _context.SaveChangesAsync();

            return RedirectToAction("Analyze");
        }
    }
}
