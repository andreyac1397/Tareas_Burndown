using Microsoft.AspNetCore.Mvc;

namespace ProyectoTareasScrum.Controladores;

public class InicioController : Controller
{
    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Error()
    {
        return View();
    }
}
