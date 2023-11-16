namespace WebAPI.DataLogic
{
		// Clase Task.cs: contiene la clase principal del modelo de tareas.
		public class Tasks
		{
			public int Id { get; set; }
			public string Titulo { get; set; }
			public string Descripcion { get; set; }
			public DateTime FechaRegistro { get; set; }
			public bool TareaCompletada { get; set; }
			public DateTime? FechaTerminada { get; set; }
		}
}
