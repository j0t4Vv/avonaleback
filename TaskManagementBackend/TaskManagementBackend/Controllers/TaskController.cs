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
        public async Task<ActionResult<Task>> CreateTask([FromBody] Task newTask)
        {
            var task = await _taskService.CreateTaskAsync(newTask);

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
        public async Task<IActionResult> UpdateTask(Guid id, [FromBody] Task updatedTask)
        {
            var task = await _taskService.UpdateTaskAsync(id, updatedTask);

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
        public async Task<ActionResult<Task>> GetTaskById(Guid id)
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
