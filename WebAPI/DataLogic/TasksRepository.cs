using Microsoft.Data.SqlClient;

namespace WebAPI.DataLogic
{
	//Clase de acceso a datos (TasksRepository.cs):
	//Contiene la logica y los metodos para conectar con la base de datos.
	public class TasksRepository
	{
		//Conexion Local - Cambiar
		private string connectionString = "Data Source=DESKTOP-9R241EJ;Initial Catalog=TaskDB;Integrated Security=True;\r\n";

		public List<Tasks> RetrieveAllTask()
		{
			List<Tasks> tareas = new List<Tasks>();

			using (SqlConnection connection = new SqlConnection(connectionString))
			{
				connection.Open();

				using (SqlCommand command = new SqlCommand("SELECT * FROM Task", connection))
				{
					using (SqlDataReader reader = command.ExecuteReader())
					{
						while (reader.Read())
						{
							Tasks tarea = new Tasks
							{
								Id = Convert.ToInt32(reader["Id"]),
								Titulo = Convert.ToString(reader["Titulo"]),
								Descripcion = Convert.ToString(reader["Descripcion"]),
								FechaRegistro = Convert.ToDateTime(reader["FechaRegistro"]),
								TareaCompletada = Convert.ToBoolean(reader["TareaCompletada"]),
								FechaTerminada = reader["FechaTerminada"] is DBNull ? (DateTime?)null : Convert.ToDateTime(reader["FechaTerminada"])
							};

							tareas.Add(tarea);
						}
					}
				}
			}

			return tareas;
		}

		public void CreateTask(Tasks newTask)
		{
			using (SqlConnection connection = new SqlConnection(connectionString))
			{
				connection.Open();

				using (SqlCommand command = new SqlCommand("INSERT INTO Task (Titulo, Descripcion, FechaRegistro, TareaCompletada, FechaTerminada) VALUES (@Titulo, @Descripcion, @FechaRegistro, @TareaCompletada, @FechaTerminada)", connection))
				{
					command.Parameters.AddWithValue("@Titulo", newTask.Titulo);
					command.Parameters.AddWithValue("@Descripcion", newTask.Descripcion);
					command.Parameters.AddWithValue("@FechaRegistro", newTask.FechaRegistro);
					command.Parameters.AddWithValue("@TareaCompletada", newTask.TareaCompletada);
					command.Parameters.AddWithValue("@FechaTerminada", (object)newTask.FechaTerminada ?? DBNull.Value);

					command.ExecuteNonQuery();
				}
			}
		}

		public void UpdateTask(Tasks taskUpdate)
		{
			using (SqlConnection connection = new SqlConnection(connectionString))
			{
				connection.Open();

				using (SqlCommand command = new SqlCommand("UPDATE Task SET Titulo = @Titulo, Descripcion = @Descripcion, FechaRegistro = @FechaRegistro, TareaCompletada = @TareaCompletada, FechaTerminada = @FechaTerminada WHERE Id = @Id", connection))
				{
					command.Parameters.AddWithValue("@Titulo", taskUpdate.Titulo);
					command.Parameters.AddWithValue("@Descripcion", taskUpdate.Descripcion);
					command.Parameters.AddWithValue("@FechaRegistro", taskUpdate.FechaRegistro);
					command.Parameters.AddWithValue("@TareaCompletada", taskUpdate.TareaCompletada);
					command.Parameters.AddWithValue("@FechaTerminada", taskUpdate.FechaTerminada ?? (object)DBNull.Value);

					command.ExecuteNonQuery();
				}
			}
		}

		public void DeleteTask(int IdTask)
		{
			using (SqlConnection connection = new SqlConnection(connectionString))
			{
				connection.Open();
				using (SqlCommand command = new SqlCommand("DELETE FROM Task WHERE Id = @Id", connection))
				{
					command.Parameters.AddWithValue("@Id", IdTask);
					command.ExecuteNonQuery();
				}
			}
		}

		public List<Tasks> RetrieveAllPendingTask()
		{
			// Implementa la lógica para obtener todas las tareas pendientes
			List<Tasks> tareasPendientes = new List<Tasks>();

			using (SqlConnection connection = new SqlConnection(connectionString))
			{
				connection.Open();

				using (SqlCommand command = new SqlCommand("SELECT * FROM Task WHERE TareaCompletada = 0", connection))
				{
					using (SqlDataReader reader = command.ExecuteReader())
					{
						while (reader.Read())
						{
							Tasks tarea = CrearTareaDesdeReader(reader);
							tareasPendientes.Add(tarea);
						}
					}
				}
			}

			return tareasPendientes;
		}

		public List<Tasks> RetrieveAllCompleteTask()
		{
			// Implementa la lógica para obtener todas las tareas completadas
			List<Tasks> tareasCompletadas = new List<Tasks>();

			using (SqlConnection connection = new SqlConnection(connectionString))
			{
				connection.Open();

				using (SqlCommand command = new SqlCommand("SELECT * FROM Task WHERE TareaCompletada = 1", connection))
				{
					using (SqlDataReader reader = command.ExecuteReader())
					{
						while (reader.Read())
						{
							Tasks tarea = CrearTareaDesdeReader(reader);
							tareasCompletadas.Add(tarea);
						}
					}
				}
			}

			return tareasCompletadas;
		}

		public Tasks RetrieveTask(int IdTask)
		{
			// Implementa la lógica para obtener una tarea por su ID
			using (SqlConnection connection = new SqlConnection(connectionString))
			{
				connection.Open();

				using (SqlCommand command = new SqlCommand("SELECT * FROM Task WHERE Id = @Id", connection))
				{
					command.Parameters.AddWithValue("@Id", IdTask);

					using (SqlDataReader reader = command.ExecuteReader())
					{
						if (reader.Read())
						{
							return CrearTareaDesdeReader(reader);
						}
					}
				}
			}

			return null;
		}

		public void CheckCompleteTask(int IdTask)
		{
			// Implementa la lógica para marcar una tarea como completada
			using (SqlConnection connection = new SqlConnection(connectionString))
			{
				connection.Open();

				using (SqlCommand command = new SqlCommand("UPDATE Task SET TareaCompletada = 1, FechaTerminada = GETDATE() WHERE Id = @Id", connection))
				{
					command.Parameters.AddWithValue("@Id", IdTask);

					command.ExecuteNonQuery();
				}
			}

		}

		// Método auxiliar para crear una tarea desde un SqlDataReader
		private Tasks CrearTareaDesdeReader(SqlDataReader reader)
		{
			return new Tasks
			{
				Id = Convert.ToInt32(reader["Id"]),
				Titulo = Convert.ToString(reader["Titulo"]),
				Descripcion = Convert.ToString(reader["Descripcion"]),
				FechaRegistro = Convert.ToDateTime(reader["FechaRegistro"]),
				TareaCompletada = Convert.ToBoolean(reader["TareaCompletada"]),
				FechaTerminada = reader["FechaTerminada"] is DBNull ? (DateTime?)null : Convert.ToDateTime(reader["FechaTerminada"])
			};
		}
	}
}
