// Controllers/RequestController.cs
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PdfAnalysisApp.Data;
using PdfAnalysisApp.Models;

public class RequestController : Controller
{
    private readonly AppDbContext _context;

    public RequestController(AppDbContext context)
    {
        _context = context;
    }

    // Exibe o formulário para o requerente
    [HttpGet]
    public IActionResult Create() => View();

     // Método para listar os requests pendentes
    public async Task<IActionResult> Index()
    {
        var requests = await _context.Requests.Where(r => r.Status == "Em análise").ToListAsync();
        return View(requests);
    }


    // Processa o formulário de submissão
    [HttpPost]
    public async Task<IActionResult> Create(IFormFile document, string name, string email, string phone)
    {
        if (document != null && document.Length > 0)
        {
            // Salvar documento no servidor
            var fileName = Path.GetFileName(document.FileName);
            var documentPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploads", fileName);

            using (var stream = new FileStream(documentPath, FileMode.Create))
            {
                await document.CopyToAsync(stream);
            }

            // Criar uma nova solicitação
            var request = new Request
            {
                Name = name,
                Email = email,
                Phone = phone,
                DocumentPath = fileName,
            };

            _context.Requests.Add(request);
            await _context.SaveChangesAsync();

            return RedirectToAction("Status", new { id = request.Id });
        }

        return View();  // Retorna à view se falhar
    }

    // Visualizar o status da solicitação
    [HttpGet]
    public async Task<IActionResult> Status(int id)
    {
        var request = await _context.Requests.FindAsync(id);
        if (request == null) return NotFound();

        return View(request);
    }
    [HttpPost]
    public async Task<IActionResult> Approve(int id)
    {
        var request = await _context.Requests.FindAsync(id);
        if (request == null) return NotFound();

        request.Status = "Aprovado";
        await _context.SaveChangesAsync();

        return RedirectToAction("Status", new { id });
    }

    [HttpPost]
    public async Task<IActionResult> Reject(int id)
    {
        var request = await _context.Requests.FindAsync(id);
        if (request == null) return NotFound();

        request.Status = "Rejeitado";
        await _context.SaveChangesAsync();

        return RedirectToAction("Status", new { id });
    }

}
