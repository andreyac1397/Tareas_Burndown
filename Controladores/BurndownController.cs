using Microsoft.AspNetCore.Mvc;
using ProyectoTareasScrum.Servicios;
using System.Text.Json;

namespace ProyectoTareasScrum.Controladores;

public class BurndownController : Controller
{
    private readonly BurndownExcelServicio _burndownExcelServicio;

    public BurndownController(BurndownExcelServicio burndownExcelServicio)
    {
        _burndownExcelServicio = burndownExcelServicio;
    }

    public IActionResult Index()
    {
        var puntos = _burndownExcelServicio.ObtenerPuntos();
        ViewBag.DatosBurndown = JsonSerializer.Serialize(puntos);
        return View(puntos);
    }
}
