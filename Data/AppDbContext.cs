// Data/AppDbContext.cs
using Microsoft.EntityFrameworkCore;
using PdfAnalysisApp.Models;

namespace PdfAnalysisApp.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) {}

        public DbSet<PdfFile> PdfFiles { get; set; }
        public DbSet<Request> Requests { get; set; }  // Tabela Requests
    }

}