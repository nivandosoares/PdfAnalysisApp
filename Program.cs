// Program.cs
using Microsoft.EntityFrameworkCore;
using PdfAnalysisApp.Data;

var builder = WebApplication.CreateBuilder(args);

// Registre o DbContext e os serviços MVC
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure o middleware necessário
app.UseStaticFiles(); // Para servir arquivos estáticos (CSS, JS, etc.)

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthorization();

// Mapeamento de rotas
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Pdf}/{action=Upload}/{id?}");


app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
