using Microsoft.EntityFrameworkCore;
using TaskManagementAPI.Data;
using TaskManagementAPI.Models;

namespace TaskManagementAPI.Services
{
    public class HistoryService
    {
        private readonly AppDbContext _context;

        public HistoryService(AppDbContext context)
        {
            _context = context;
        }

        // Registra um novo histórico
        public async Task AddHistoryAsync(TaskHistory taskHistory)
        {
            _context.TaskHistories.Add(taskHistory);
            await _context.SaveChangesAsync();
        }

        // Obtém o histórico de uma tarefa específica
        public async Task<IEnumerable<TaskHistory>> GetHistoryAsync(Guid taskId)
        {
            return await _context.TaskHistories
                .Where(h => h.TaskId == taskId)
                .OrderBy(h => h.CreatedAt)
                .ToListAsync();
        }
    }
}
