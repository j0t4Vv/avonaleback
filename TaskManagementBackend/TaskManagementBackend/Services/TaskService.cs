using Microsoft.EntityFrameworkCore;
using TaskManagementAPI.Data;
using TaskManagementAPI.Models;

namespace TaskManagementAPI.Services
{
    public class TaskService
    {
        private readonly AppDbContext _context;

        public TaskService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<TaskItem>> GetAllTasksAsync() //Removido o "?" do "TaskItem?", no caso ao fazer isso o valor não pode ser nulo, se for necessário que o valor seja nulo, é necessário adicionar o "?" novamente
        {
            return await _context.Tasks.Include(t => t.CreatedBy).ToListAsync();
        }

        public async Task<TaskItem?> GetTaskByIdAsync(int id)
        {
            return await _context.Tasks.Include(t => t.CreatedBy).FirstOrDefaultAsync(t => t.Id == id);
        }

        public async Task<TaskItem> CreateTaskAsync(TaskItem task, int userId)
        {
            task.CreatedById = userId;
            _context.Tasks.Add(task);
            await _context.SaveChangesAsync();
            return task;
        }

        public async Task<TaskItem> UpdateTaskAsync(TaskItem task, int userId) //Removido o "?" do "TaskItem?", no caso ao fazer isso o valor não pode ser nulo
        {
            var existingTask = await _context.Tasks.FindAsync(task.Id);
            if (existingTask == null) return null;

            existingTask.Title = task.Title;
            existingTask.Description = task.Description;
            existingTask.IsUrgent = task.IsUrgent;
            existingTask.Status = task.Status;

            _context.TaskHistories.Add(new TaskHistory
            {
                TaskId = task.Id,
                Description = "Task updated",
                ChangedBy = userId
            });

            await _context.SaveChangesAsync();
            return existingTask;
        }

        public async Task<bool> DeleteTaskAsync(int id)
        {
            var task = await _context.Tasks.FindAsync(id);
            if (task == null) return false;

            _context.Tasks.Remove(task);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}