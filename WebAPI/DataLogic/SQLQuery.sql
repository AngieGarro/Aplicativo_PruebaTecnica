-- Script para crear la base de datos
CREATE DATABASE TaskDB;
GO

-- Seleccionar la base de datos
USE TaskDB;
GO

-- Script para crear la tabla Tareas
CREATE TABLE Task (
    Id INT PRIMARY KEY IDENTITY(1,1),
    Titulo NVARCHAR(100) NOT NULL,
    Descripcion NVARCHAR(250),
    FechaRegistro DATETIME NOT NULL,
    TareaCompletada BIT NOT NULL DEFAULT 0,
    FechaTerminada DATETIME
);
GO

--Listar todas las tareas
SELECT * FROM Task


-- Insertar una tarea en la tabla Tareas
INSERT INTO Task (Titulo, Descripcion, FechaRegistro, TareaCompletada, FechaTerminada)
VALUES ('Tarea de ejemplo', 'Esta es una tarea de ejemplo.', GETDATE(), 0, NULL);

-- Modificar una tarea en la tabla Tareas
UPDATE Task
SET
    Titulo = 'Nuevo título',
    Descripcion = 'Nueva descripción',
    TareaCompletada = 1, 
    FechaTerminada = GETDATE() 
WHERE Id = 1; -- El ID de la tarea que deseas modificar


-- Consultar la tarea modificada
SELECT * FROM Task WHERE Id = 1;

-- Eliminar una tarea de la tabla Tareas
DELETE FROM Task
WHERE Id = 1; -- El ID de la tarea que deseas eliminar

-- Obtener todas las tareas pendientes
SELECT *
FROM Task
WHERE TareaCompletada = 0; -- Filtrar las tareas no completadas

-- Obtener todas las tareas completadas
SELECT *
FROM Task
WHERE TareaCompletada = 1; -- Filtrar las tareas completadas

-- Obtener una tarea específica por su Id
SELECT *
FROM Task
WHERE Id = 1; 

-- Marcar una tarea como completada
UPDATE Task
SET
    TareaCompletada = 1,
    FechaTerminada = GETDATE()
WHERE Id = 1; 
