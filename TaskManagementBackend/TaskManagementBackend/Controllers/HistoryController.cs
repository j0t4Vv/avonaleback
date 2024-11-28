using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TaskManagementAPI.Data;

namespace TaskManagementAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HistoryController : ControllerBase
    {
        private readonly AppDbContext _context;

        public HistoryController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet("{taskId}")]
        [Authorize]
        public async Task<IActionResult> GetHistory(int taskId)
        {
            var history = await _context.TaskHistories
                .Where(h => h.TaskId == taskId)
                .Include(h => h.ChangedBy)
                .ToListAsync();
            return Ok(history);
        }
    }
}