using CursoYoutubeProgramadorTech.Data;
using CursoYoutubeProgramadorTech.Helper;
using CursoYoutubeProgramadorTech.Repositories;
using CursoYoutubeProgramadorTech.Repository;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Configuração para o banco de dados
var configuration = builder.Configuration;

builder.Services.AddDbContext<ConnectionContext>(options =>
{
    options.UseNpgsql(configuration.GetConnectionString("Database"));
});

// personalizado para sessions
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

builder.Services.AddScoped<IContactRepository, ContactRespository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();

// personalizado para sessions
builder.Services.AddScoped<ISessionUser, Session>();

builder.Services.AddScoped<IEmail, Email>();

// Adicionar serviço de sessão
builder.Services.AddSession(o =>
{
    o.Cookie.HttpOnly = true;
    o.Cookie.IsEssential = true;
});

// -----------------------------------

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

// Configurar middleware de sessão
app.UseSession();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Login}/{action=Index}/{id?}");

app.Run();
