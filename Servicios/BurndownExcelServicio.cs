using ClosedXML.Excel;
using ProyectoTareasScrum.Modelos;

namespace ProyectoTareasScrum.Servicios;

public class BurndownExcelServicio
{
    private readonly IWebHostEnvironment _entorno;

    public BurndownExcelServicio(IWebHostEnvironment entorno)
    {
        _entorno = entorno;
    }

    public List<PuntoBurndown> ObtenerPuntos()
    {
        var ruta = Path.Combine(_entorno.ContentRootPath, "Datos", "Burndown", "burndown.xlsx");

        if (!File.Exists(ruta))
        {
            return ObtenerDatosPorDefecto();
        }

        using var libro = new XLWorkbook(ruta);
        var hoja = libro.Worksheet("Burndown");
        var filas = hoja.RangeUsed()?.RowsUsed().Skip(1) ?? Enumerable.Empty<IXLRangeRow>();

        var puntos = new List<PuntoBurndown>();

        foreach (var fila in filas)
        {
            var dia = fila.Cell(1).GetString().Trim();

            if (string.IsNullOrWhiteSpace(dia))
            {
                continue;
            }

            puntos.Add(new PuntoBurndown
            {
                Dia = dia,
                Ideal = fila.Cell(2).GetValue<int>(),
                Real = fila.Cell(3).GetValue<int>()
            });
        }

        return puntos.Count > 0 ? puntos : ObtenerDatosPorDefecto();
    }

    private static List<PuntoBurndown> ObtenerDatosPorDefecto()
    {
        return new List<PuntoBurndown>
        {
            new() { Dia = "Lunes", Ideal = 13, Real = 13 },
            new() { Dia = "Martes", Ideal = 10, Real = 11 },
            new() { Dia = "Miércoles", Ideal = 7, Real = 8 },
            new() { Dia = "Jueves", Ideal = 3, Real = 5 },
            new() { Dia = "Viernes", Ideal = 0, Real = 0 }
        };
    }
}
