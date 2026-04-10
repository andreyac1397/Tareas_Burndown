using System.ComponentModel.DataAnnotations;

namespace ProyectoTareasScrum.Modelos;

public class Tarea
{
    public int TareaID { get; set; }

    [Required(ErrorMessage = "El título es obligatorio.")]
    [StringLength(150, ErrorMessage = "Máximo 150 caracteres.")]
    public string Titulo { get; set; } = string.Empty;

    [StringLength(300, ErrorMessage = "Máximo 300 caracteres.")]
    public string? Descripcion { get; set; }

    [Required(ErrorMessage = "La prioridad es obligatoria.")]
    public string Prioridad { get; set; } = "Media";

    [DataType(DataType.Date)]
    public DateTime? FechaLimite { get; set; }

    public bool Completada { get; set; }

    public DateTime FechaCreacion { get; set; }
    public DateTime? FechaActualizacion { get; set; }

    public bool EstaVencida =>
        !Completada &&
        FechaLimite.HasValue &&
        FechaLimite.Value.Date < DateTime.Today;
}
