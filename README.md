# ProyectoTareasScrum

Proyecto web sencillo en **ASP.NET Core MVC** para:
- Gestionar tareas personales (To-Do List)
- Mostrar un **Burndown Chart** leyendo un archivo Excel local

## Tecnologías
- ASP.NET Core MVC
- SQL Server
- ADO.NET (`Microsoft.Data.SqlClient`)
- ClosedXML para leer Excel
- Chart.js para el gráfico

## Estructura
- `Controladores` → controladores MVC
- `Modelos` → clases del sistema
- `Datos` → acceso a SQL Server
- `Servicios` → lectura del Excel para Burndown
- `Vistas` → interfaz
- `BaseDeDatos` → scripts SQL
- `Datos/Burndown/burndown.xlsx` → archivo fuente del gráfico

## Base de datos
1. Ejecuta `BaseDeDatos/query_bd_tareas.sql`
2. Si quieres datos de prueba, ejecuta `BaseDeDatos/query_datos_iniciales.sql`
3. Cambia la cadena de conexión en `appsettings.json`

## Burndown
El gráfico toma los datos de:
`Datos/Burndown/burndown.xlsx`

Formato esperado:
- Columna A → Dia
- Columna B → Ideal
- Columna C → Real

## Ejecutar local
1. Restaurar paquetes
2. Ejecutar el proyecto
3. Entrar a `/Tareas` y `/Burndown`

## Publicar en Plesk (IIS)
1. En tu PC:
   ```bash
   dotnet restore
   dotnet publish -c Release -o publish
   ```
2. Sube **el contenido interno** de la carpeta `publish` a `httpdocs`
3. Verifica:
   - que la cadena de conexión sea la del servidor
   - que `Datos/Burndown/burndown.xlsx` sí se haya copiado
   - que el hosting tenga runtime .NET 8 o el que vayas a usar

## Nota
No se guarda nada del gráfico en la base de datos porque se genera desde el archivo Excel.
