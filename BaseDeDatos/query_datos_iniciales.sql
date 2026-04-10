USE DB_TareasPersonales;
GO

INSERT INTO Tareas (Titulo, Descripcion, Prioridad, FechaLimite, Completada)
VALUES
('Preparar exposición SCRUM', 'Organizar backlog y sprint backlog', 'Alta', '2026-04-11', 0),
('Revisar Jira del equipo', 'Verificar estados y puntos de historia', 'Media', '2026-04-12', 1),
('Subir proyecto a Plesk', 'Publicar la aplicación para la demo', 'Alta', '2026-04-13', 0);
GO
