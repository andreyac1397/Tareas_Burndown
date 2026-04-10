using Microsoft.AspNetCore.Mvc;
using ProyectoTareasScrum.Datos;
using ProyectoTareasScrum.Modelos;

namespace ProyectoTareasScrum.Controladores;

public class TareasController : Controller
{
    private readonly TareaDatos _tareaDatos;

    public TareasController(TareaDatos tareaDatos)
    {
        _tareaDatos = tareaDatos;
    }

    public async Task<IActionResult> Index()
    {
        var tareas = await _tareaDatos.ListarAsync();
        return View(tareas);
    }

    [HttpGet]
    public IActionResult Crear()
    {
        return View(new Tarea());
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Crear(Tarea tarea)
    {
        if (!ModelState.IsValid)
        {
            return View(tarea);
        }

        await _tareaDatos.CrearAsync(tarea);
        TempData["Mensaje"] = "Tarea creada correctamente.";
        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    public async Task<IActionResult> Editar(int id)
    {
        var tarea = await _tareaDatos.ObtenerPorIdAsync(id);

        if (tarea is null)
        {
            return NotFound();
        }

        return View(tarea);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Editar(Tarea tarea)
    {
        if (!ModelState.IsValid)
        {
            return View(tarea);
        }

        await _tareaDatos.EditarAsync(tarea);
        TempData["Mensaje"] = "Tarea actualizada correctamente.";
        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> CambiarEstado(int id)
    {
        await _tareaDatos.CambiarEstadoAsync(id);
        TempData["Mensaje"] = "Estado de la tarea actualizado.";
        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Eliminar(int id)
    {
        await _tareaDatos.EliminarAsync(id);
        TempData["Mensaje"] = "Tarea eliminada correctamente.";
        return RedirectToAction(nameof(Index));
    }
}
