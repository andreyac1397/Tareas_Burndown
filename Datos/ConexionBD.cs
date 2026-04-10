using Microsoft.Data.SqlClient;

namespace ProyectoTareasScrum.Datos;

public class ConexionBD
{
    private readonly string _cadenaConexion;

    public ConexionBD(IConfiguration configuracion)
    {
        _cadenaConexion = configuracion.GetConnectionString("DefaultConnection")
    ?? throw new InvalidOperationException("No se encontró la cadena de conexión.");
    }

    public SqlConnection CrearConexion()
    {
        return new SqlConnection(_cadenaConexion);
    }
}
