using Microsoft.AspNetCore.Mvc;
using TaskManagementAPI.Models;
using TaskManagementAPI.Services;

namespace TaskManagementAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        private readonly TaskService _taskService;
        private readonly HistoryService _historyService;

        public TaskController(TaskService taskService, HistoryService historyService)
        {
            _taskService = taskService;
            _historyService = historyService;
        }

        // Criação de uma nova tarefa
        [HttpPost]
        public async Task<ActionResult<Task>> CreateTask([FromBody] TaskItem newTask)
        {
            var user = new User(); // instância do usuário que criou a tarefa
            var task = await _taskService.CreateTaskAsync(newTask, user.Id); // esse método pede o id do usuário que criou a tarefa, então fiz uma instância do user e passei o id dele, se tiver uma outra lógica so mudar

            // Registro no histórico
            var history = new TaskHistory
            {
                TaskId = task.Id,
                Action = "Criada",
                Description = $"Tarefa '{task.Title}' foi criada.",
                CreatedAt = DateTime.UtcNow
            };
            await _historyService.AddHistoryAsync(history);

            return CreatedAtAction(nameof(GetTaskById), new { id = task.Id }, task);
        }

        // Atualização de uma tarefa existente
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTask(int id, [FromBody] TaskItem updatedTask)
        {
            var task = await _taskService.UpdateTaskAsync(updatedTask, id);

            // Registro no histórico
            var history = new TaskHistory
            {
                TaskId = task.Id,
                Action = "Atualizada",
                Description = $"Tarefa '{task.Title}' foi atualizada.",
                CreatedAt = DateTime.UtcNow
            };
            await _historyService.AddHistoryAsync(history);

            return NoContent();
        }

        // Obter uma tarefa específica
        [HttpGet("{id}")]
        public async Task<ActionResult<Task>> GetTaskById(int id)
        {
            var task = await _taskService.GetTaskByIdAsync(id);
            if (task == null)
            {
                return NotFound();
            }
            return Ok(task);
        }
    }
}
