// Models/Request.cs
namespace PdfAnalysisApp.Models
{
    public class Request
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public required string Email { get; set; }
        public required string Phone { get; set; }
        public string Status { get; set; } = "Em análise";
        public required string DocumentPath { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}