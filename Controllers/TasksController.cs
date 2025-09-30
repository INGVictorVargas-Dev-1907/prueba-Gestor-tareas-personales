using GestorPersonalTareas.Data;
using GestorPersonalTareas.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GestorPersonalTareas.Controllers
{
    /// <summary>
    /// Controladro para la gestion de tareas personales
    /// Crud
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class TasksController : Controller
    {
        private readonly TaskDbContext _context;

        public TasksController(TaskDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Obtener todas las tareas
        /// </summary>
        /// <returns>Lista de tareas</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<ApiResponse<IEnumerable<TaskItem>>>> GetTasks()
        {
            var tasks = await _context.Tasks.ToListAsync();
            var response = new ApiResponse<IEnumerable<TaskItem>>
            {
                Success = true,
                Message = "Tareas obtenidas correctamente",
                Data = tasks
            };

            return Ok(response);
        }

        /// <summary>
        /// Obtener una tarea por ID
        /// </summary>
        /// <param name="id">ID de la tarea</param>
        /// <returns>Tarea encontrada</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ApiResponse<TaskItem>>> GetTaskById(int id)
        {
            try
            {
                var task = await _context.Tasks.FindAsync(id);

                if (task == null)
                {
                    return NotFound(new ApiResponse<TaskItem>
                    {
                        Success = false,
                        Message = "Tarea no encontrada",
                        Data = null
                    });
                }

                return Ok(new ApiResponse<TaskItem>
                {
                    Success = true,
                    Message = "Tarea obtenida correctamente",
                    Data = task
                });

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ApiResponse<TaskItem>
                {
                    Success = false,
                    Message = $"Error al obtener la tarea: {ex.Message}",
                    Data = null
                });


            }

        }

        /// <sumary>
        /// /// Crear una nueva tarea
        /// </summary>
        /// <param name="task">Datos de la tarea</param>
        /// <returns>Tarea creada</returns>
        [HttpPost("guardar")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ApiResponse<TaskItem>>> CreateTask(TaskItem task)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            _context.Tasks.Add(task);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetTaskById), new { id = task.Id }, new ApiResponse<TaskItem>
            {
                Success = true,
                Message = "Tarea creada correctamente",
                Data = task
            });
        }

        // <summary>
        /// Actualizar el estado de una tarea (completada o pendiente)
        /// </summary>
        /// <param name="id">ID de la tarea</param>
        /// <param name="updatedTask">Objeto con el nuevo estado</param>
        /// <returns>Resultado de la operación</returns>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ApiResponse<TaskItem>>> UpdateTask(int id, [FromBody] TaskItem updatedTask)
        {
            try
            {
                var task = await _context.Tasks.FindAsync(id);
                if (task == null)
                {
                    return NotFound(new ApiResponse<TaskItem>
                    {
                        Success = false,
                        Message = "Tarea no encontrada",
                        Data = null
                    });
                }

                task.Title = updatedTask.Title;
                task.IsCompleted = updatedTask.IsCompleted;

                await _context.SaveChangesAsync();

                return Ok(new ApiResponse<TaskItem>
                {
                    Success = true,
                    Message = "Tarea actualizada correctamente",
                    Data = task
                });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ApiResponse<TaskItem>
                {
                    Success = false,
                    Message = $"Error al actualizar la tarea: {ex.Message}",
                    Data = null
                });
            }
        }

        /// <summary>
        /// Marcar una tarea como completada
        /// </summary>
        /// <param name="id">ID de la tarea</param>
        /// <returns>Resultado de la operación</returns>
        [HttpPut("{id}/complete")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> CompleteTask(int id)
        {
            try
            {
                var task = await _context.Tasks.FindAsync(id);
                if (task == null)
                {
                    return NotFound(new ApiResponse<TaskItem>
                    {
                        Success = false,
                        Message = "Tarea no encontrada",
                        Data = null
                    });
                }

                task.IsCompleted = true;
                await _context.SaveChangesAsync();

                return Ok(new ApiResponse<TaskItem>
                {
                    Success = true,
                    Message = "Tarea marcada como completada",
                    Data = task
                });

            } catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ApiResponse<TaskItem>
                {
                    Success = false,
                    Message = $"Error al completar la tarea: {ex.Message}",
                    Data = null
                });
            }
        }

        /// <summary>
        /// Eliminar una tarea
        /// </summary>
        /// <param name="id">ID de la tarea</param>
        /// <returns>Resultado de la eliminación</returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ApiResponse<TaskItem>>> DeleteTask(int id)
        {
            try
            {
                var task = await _context.Tasks.FindAsync(id);

                if (task == null)
                {
                    return NotFound(new ApiResponse<TaskItem>
                    {
                        Success = false,
                        Message = "Tarea no encontrada",
                        Data = null
                    });
                }

                _context.Tasks.Remove(task);
                await _context.SaveChangesAsync();

                return Ok(new ApiResponse<TaskItem>
                {
                    Success = true,
                    Message = "Tarea eliminada exitosamente",
                    Data = task
                });

            } catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ApiResponse<TaskItem>
                {
                    Success = false,
                    Message = $"Error al eliminar la tarea: {ex.Message}",
                    Data = null
                });
            }
        }
    }
}
