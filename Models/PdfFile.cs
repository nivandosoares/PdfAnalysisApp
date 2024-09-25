// Models/PdfFile.cs
using System;

namespace PdfAnalysisApp.Models
{
    public class PdfFile
    {
        public int Id { get; set; }
        public string FileName { get; set; }
        public string FilePath { get; set; }
        public string Status { get; set; } = "Pendente"; // Pending, Approved, Rejected
        public string Feedback { get; set; }
        public DateTime UploadedAt { get; set; } = DateTime.Now;
    }
}
