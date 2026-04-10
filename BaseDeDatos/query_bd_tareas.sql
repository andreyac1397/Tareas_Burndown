CREATE DATABASE DB_TareasPersonales;
GO

USE DB_TareasPersonales;
GO

CREATE TABLE Tareas (
    TareaID INT IDENTITY(1,1) PRIMARY KEY,
    Titulo NVARCHAR(150) NOT NULL,
    Descripcion NVARCHAR(300) NULL,
    Prioridad NVARCHAR(10) NOT NULL
        CHECK (Prioridad IN ('Alta', 'Media', 'Baja')),
    FechaLimite DATE NULL,
    Completada BIT NOT NULL DEFAULT 0,
    FechaCreacion DATETIME NOT NULL DEFAULT GETDATE(),
    FechaActualizacion DATETIME NULL
);
GO
