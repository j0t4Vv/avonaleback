using Microsoft.AspNetCore.Mvc;
using TaskManagerBackend.Models;
using TaskManagerBackend.Dtos;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace TaskManagerBackend.Controllers
{
    [ApiController]
    [Route("api/tasks")]
    public class TaskController : ControllerBase
    {
        private readonly AppDbContext _context;

        public TaskController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetTasks()
        {
            var tasks = await _context.Tasks.Include(t => t.User).ToListAsync();
            return Ok(tasks);
        }

        [HttpPost]
        public IActionResult AddTask(TaskDto taskDto)
        {
            var task = new Task
            {
                Title = taskDto.Title,
                Description = taskDto.Description,
                Level = taskDto.Level,
                Status = taskDto.Status,
                UserId = taskDto.UserId
            };

            _context.Tasks.Add(task);
            _context.SaveChanges();
            return Ok(task);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateTask(int id, TaskDto taskDto)
        {
            var task = _context.Tasks.Find(id);
            if (task == null) return NotFound("Tarefa não encontrada.");

            task.Title = taskDto.Title;
            task.Description = taskDto.Description;
            task.Level = taskDto.Level;
            task.Status = taskDto.Status;

            _context.SaveChanges();
            return Ok(task);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteTask(int id)
        {
            var task = _context.Tasks.Find(id);
            if (task == null) return NotFound("Tarefa não encontrada.");

            _context.Tasks.Remove(task);
            _context.SaveChanges();
            return Ok("Tarefa deletada com sucesso.");
        }
    }
}
