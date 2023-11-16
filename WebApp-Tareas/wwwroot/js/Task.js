//JS que incluye las funciones ajax y los js de la clase Task.

/// Constante para la URL del API
const ApiURL = 'https://localhost:7094'; 

// Funcion para obtener todas las tareas
function obtenerTodasLasTareas() {
    $.ajax({
        url: `${ApiURL}/api/Task/ObtenerTodasLasTareas`,
        method: 'GET',
        success: function (tareas) {
            mostrarTareas(tareas);
        },
        error: function (error) {
            console.error('Error al obtener tareas:', error);
        }
    });
}

// Funcion para mostrar las tareas en el frontend
function mostrarTareas(tareas) {
    var container = $('#Task-container');
    container.empty();

    tareas.forEach(function (tarea) {
        var tareaElement = $('<div>');
        tareaElement.html(`
            <p>
                <strong>${tarea.Titulo}</strong> - ${tarea.Descripcion}
                <strong> ${tarea.FechaRegistro} - </strong> ${tarea.TareaCompletada}
                <strong>${tarea.FechaTerminada} - </strong> 
                <button onclick="marcarComoCompletada(${tarea.Id})">Completada</button>
                <button onclick="editarTarea(${tarea.Id})">Editar</button>
                <button onclick="eliminarTarea(${tarea.Id})">Eliminar</button>
            </p>
        `);
        container.append(tareaElement);
    });
}

// Funcion para crear una nueva tarea
function crearTarea() {
    var titulo = $('#txtTitulo').val();
    var descripcion = $('#txtDescripcion').val();
    var fechaRegistro = $('#txtFechaRegistro')
    var status = $('#optionsStatus')
    var fechaFinalizado = $('#txtTareaCompletada')

    $.ajax({
        url: `${ApiURL}/api/Task/CrearTarea`, 
        method: 'POST',
        data: {
            Titulo: titulo, Descripcion: descripcion, FechaRegistro: fechaRegistro, TareaCompletada: status
            FechaTerminada: fechaFinalizado },
        success: function () {
            obtenerTodasLasTareas();
            limpiarFormulario();
        },
        error: function (error) {
            console.error('Error al crear tarea:', error);
        }
    });
}

// Funcion para limpiar el formulario despues de crear una tarea
function limpiarFormulario() {
    $('#titulo').val('');
    $('#descripcion').val('');
    $('#fechaRegistro').val('');
    $('#status').val('');
    $('#fechaFinalizado').val('');
}

// Funcion para editar una tarea
function editarTarea(id) {
    var id = $('#txtId').val();
    var titulo = $('#txtTitulo').val();
    var descripcion = $('#txtDescripcion').val();
    var fechaRegistro = $('#txtFechaRegistro')
    var status = $('#optionsStatus')
    var fechaFinalizado = $('#txtTareaCompletada')

    $.ajax({
        url: `${ApiURL}/api/Task/ModificarTarea?id=${id}`,
        method: 'POST',
        data: {
            Titulo: titulo, Descripcion: descripcion, FechaRegistro: fechaRegistro, TareaCompletada: status
            FechaTerminada: fechaFinalizado
        },
        success: function () {
            obtenerTodasLasTareas();
            limpiarFormulario();
        },
        error: function (error) {
            console.error('Error al modificar tarea:', error);
        }
    });
}

// Funcion para eliminar una tarea
function eliminarTarea(id) {
    if (confirm('Seguro de que deseas eliminar esta tarea?')) {
        $.ajax({
            url: `${ApiURL}/api/Task/EliminarTarea/${id}`, 
            method: 'DELETE',
            success: function () {
                obtenerTodasLasTareas();
            },
            error: function (error) {
                console.error('Error al eliminar tarea:', error);
            }
        });
    }
}

// Funcion para obtener todas las tareas pendientes
function obtenerTodasLasTareasPendientes() {
    $.ajax({
        url: `${ApiURL}/api/Task/ObtenerTodasLasTareasPendientes`, 
        method: 'GET',
        success: function (tareasPendientes) {
            mostrarTareas(tareasPendientes);
        },
        error: function (error) {
            console.error('Error al obtener tareas pendientes:', error);
        }
    });
}

// Funcion para obtener todas las tareas completadas
function obtenerTodasLasTareasCompletadas() {
    $.ajax({
        url: `${ApiURL}/api/Task/ObtenerTodasLasTareasCompletadas`, 
        method: 'GET',
        success: function (tareasCompletadas) {
            mostrarTareas(tareasCompletadas);
        },
        error: function (error) {
            console.error('Error al obtener tareas completadas:', error);
        }
    });
}

// Funcion para obtener una tarea por ID
function obtenerUnaTarea(id) {
    $.ajax({
        url: `${ApiURL}/api/Task/ObtenerUnaTarea/${id}`,
        method: 'GET',
        success: function (tarea) {
            // Falta Implementar 
            // mostrarDetallesTarea(tarea);
        },
        error: function (error) {
            console.error('Error al obtener tarea:', error);
        }
    });
}

// Funcion para marcar una tarea como completada
function marcarComoCompletada(id) {
    $.ajax({
        url: `${ApiURL}/api/Task/MarcarTareaComoCompletada/${id}`, 
        method: 'PUT',
        success: function () {
            obtenerTodasLasTareas();
        },
        error: function (error) {
            console.error('Error al marcar tarea como completada:', error);
        }
    });
}

