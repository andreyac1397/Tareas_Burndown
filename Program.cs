using Microsoft.AspNetCore.Mvc.Razor;
using ProyectoTareasScrum.Datos;
using ProyectoTareasScrum.Servicios;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

builder.Services.Configure<RazorViewEngineOptions>(opciones =>
{
    opciones.ViewLocationFormats.Clear();
    opciones.ViewLocationFormats.Add("/Vistas/{1}/{0}.cshtml");
    opciones.ViewLocationFormats.Add("/Vistas/Shared/{0}.cshtml");
});

builder.Services.AddScoped<TareaDatos>();
builder.Services.AddScoped<BurndownExcelServicio>();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Inicio/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Inicio}/{action=Index}/{id?}");

app.Run();
