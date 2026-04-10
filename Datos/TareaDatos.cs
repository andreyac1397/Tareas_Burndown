using Microsoft.Data.SqlClient;
using ProyectoTareasScrum.Modelos;

namespace ProyectoTareasScrum.Datos;

public class TareaDatos
{
    private readonly ConexionBD _conexionBD;

    public TareaDatos(IConfiguration configuracion)
    {
        _conexionBD = new ConexionBD(configuracion);
    }

    public async Task<List<Tarea>> ListarAsync()
    {
        var lista = new List<Tarea>();

        await using var conexion = _conexionBD.CrearConexion();
        await conexion.OpenAsync();

        const string sql = @"
            SELECT TareaID, Titulo, Descripcion, Prioridad, FechaLimite,
                   Completada, FechaCreacion, FechaActualizacion
            FROM Tareas
            ORDER BY Completada ASC, FechaLimite ASC, FechaCreacion DESC;";

        await using var comando = new SqlCommand(sql, conexion);
        await using var lector = await comando.ExecuteReaderAsync();

        while (await lector.ReadAsync())
        {
            lista.Add(new Tarea
            {
                TareaID = lector.GetInt32(0),
                Titulo = lector.GetString(1),
                Descripcion = lector.IsDBNull(2) ? null : lector.GetString(2),
                Prioridad = lector.GetString(3),
                FechaLimite = lector.IsDBNull(4) ? null : lector.GetDateTime(4),
                Completada = lector.GetBoolean(5),
                FechaCreacion = lector.GetDateTime(6),
                FechaActualizacion = lector.IsDBNull(7) ? null : lector.GetDateTime(7)
            });
        }

        return lista;
    }

    public async Task<Tarea?> ObtenerPorIdAsync(int id)
    {
        await using var conexion = _conexionBD.CrearConexion();
        await conexion.OpenAsync();

        const string sql = @"
            SELECT TareaID, Titulo, Descripcion, Prioridad, FechaLimite,
                   Completada, FechaCreacion, FechaActualizacion
            FROM Tareas
            WHERE TareaID = @TareaID;";

        await using var comando = new SqlCommand(sql, conexion);
        comando.Parameters.AddWithValue("@TareaID", id);

        await using var lector = await comando.ExecuteReaderAsync();

        if (!await lector.ReadAsync())
        {
            return null;
        }

        return new Tarea
        {
            TareaID = lector.GetInt32(0),
            Titulo = lector.GetString(1),
            Descripcion = lector.IsDBNull(2) ? null : lector.GetString(2),
            Prioridad = lector.GetString(3),
            FechaLimite = lector.IsDBNull(4) ? null : lector.GetDateTime(4),
            Completada = lector.GetBoolean(5),
            FechaCreacion = lector.GetDateTime(6),
            FechaActualizacion = lector.IsDBNull(7) ? null : lector.GetDateTime(7)
        };
    }

    public async Task CrearAsync(Tarea tarea)
    {
        await using var conexion = _conexionBD.CrearConexion();
        await conexion.OpenAsync();

        const string sql = @"
            INSERT INTO Tareas
            (Titulo, Descripcion, Prioridad, FechaLimite, Completada)
            VALUES
            (@Titulo, @Descripcion, @Prioridad, @FechaLimite, @Completada);";

        await using var comando = new SqlCommand(sql, conexion);
        AgregarParametros(comando, tarea);
        await comando.ExecuteNonQueryAsync();
    }

    public async Task EditarAsync(Tarea tarea)
    {
        await using var conexion = _conexionBD.CrearConexion();
        await conexion.OpenAsync();

        const string sql = @"
            UPDATE Tareas
            SET Titulo = @Titulo,
                Descripcion = @Descripcion,
                Prioridad = @Prioridad,
                FechaLimite = @FechaLimite,
                Completada = @Completada,
                FechaActualizacion = GETDATE()
            WHERE TareaID = @TareaID;";

        await using var comando = new SqlCommand(sql, conexion);
        AgregarParametros(comando, tarea);
        comando.Parameters.AddWithValue("@TareaID", tarea.TareaID);

        await comando.ExecuteNonQueryAsync();
    }

    public async Task CambiarEstadoAsync(int id)
    {
        await using var conexion = _conexionBD.CrearConexion();
        await conexion.OpenAsync();

        const string sql = @"
            UPDATE Tareas
            SET Completada = CASE WHEN Completada = 1 THEN 0 ELSE 1 END,
                FechaActualizacion = GETDATE()
            WHERE TareaID = @TareaID;";

        await using var comando = new SqlCommand(sql, conexion);
        comando.Parameters.AddWithValue("@TareaID", id);

        await comando.ExecuteNonQueryAsync();
    }

    public async Task EliminarAsync(int id)
    {
        await using var conexion = _conexionBD.CrearConexion();
        await conexion.OpenAsync();

        const string sql = "DELETE FROM Tareas WHERE TareaID = @TareaID;";

        await using var comando = new SqlCommand(sql, conexion);
        comando.Parameters.AddWithValue("@TareaID", id);

        await comando.ExecuteNonQueryAsync();
    }

    private static void AgregarParametros(SqlCommand comando, Tarea tarea)
    {
        comando.Parameters.AddWithValue("@Titulo", tarea.Titulo);
        comando.Parameters.AddWithValue("@Descripcion", (object?)tarea.Descripcion ?? DBNull.Value);
        comando.Parameters.AddWithValue("@Prioridad", tarea.Prioridad);
        comando.Parameters.AddWithValue("@FechaLimite", (object?)tarea.FechaLimite ?? DBNull.Value);
        comando.Parameters.AddWithValue("@Completada", tarea.Completada);
    }
}
