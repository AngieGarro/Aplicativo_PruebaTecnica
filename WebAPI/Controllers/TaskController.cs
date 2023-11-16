using Microsoft.AspNetCore.Mvc;
using WebAPI.DataLogic;

namespace WebAPI.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class TaskController : Controller
	{
		private TasksRepository Logic = new TasksRepository();

		[HttpPost]
		[Route("CrearTarea")]
		public async Task<IActionResult> CrearTarea(Tasks nuevaTarea)
		{
			Logic.CreateTask(nuevaTarea);
			return Ok(nuevaTarea);
		}

		[HttpGet]
		[Route("ModificarTarea")]
		public async Task<IActionResult> ModificarTarea(int id)
		{
			Tasks tarea = Logic.RetrieveTask(id);
			return Ok(tarea);
		}


		[HttpGet]
		[Route("EliminarTarea")]
		public async Task<IActionResult> EliminarTarea(int id)
		{
			Tasks tarea = Logic.RetrieveTask(id);
			return Ok(tarea);
		}

		[HttpPost]
		[Route("ConfirmarEliminacionTarea")]
		public async Task<IActionResult> ConfirmarEliminacionTarea(int id)
		{
			Logic.DeleteTask(id);
			return Ok(id);
		}

        [HttpGet]
        [Route("ObtenerTodasLasTareas")]
        public async Task<IActionResult> ObtenerTodasLasTareas()
        {
            var todasLasTareas = Logic.RetrieveAllTask();
            return Ok(todasLasTareas);
        }

        [HttpGet]
		[Route("ObtenerTodasLasTareasPendientes")]
		public async Task<IActionResult> ObtenerTodasLasTareasPendientes()
		{
			var tareasPendientes = Logic.RetrieveAllPendingTask();
			return Ok(tareasPendientes);
		}

		[HttpGet]
		[Route("ObtenerTodasLasTareasCompletadas")]
		public async Task<IActionResult> ObtenerTodasLasTareasCompletadas()
		{
			var tareasCompletadas = Logic.RetrieveAllCompleteTask();
			return Ok(tareasCompletadas);
		}

		[HttpGet]
		[Route("ObtenerUnaTarea")]
		public async Task<IActionResult> ObtenerUnaTarea(int id)
		{
			Tasks tarea = Logic.RetrieveTask(id);
			return Ok(tarea);
		}

		[HttpPost]
		[Route("MarcarTareaComoCompletada")]
		public async Task<IActionResult> MarcarTareaComoCompletada(int id)
		{
			Logic.CheckCompleteTask(id);
			return Ok(id);
		}
	}
}

