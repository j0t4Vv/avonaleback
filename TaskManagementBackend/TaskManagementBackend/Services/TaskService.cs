
public class TaskService
{
    private readonly AppDbContext _context;

    public TaskService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<Task>> GetAllTasksAsync()
    {
        return await _context.Tasks.Include(t => t.CreatedBy).ToListAsync();
    }

    public async Task<Task?> GetTaskByIdAsync(int id)
    {
        return await _context.Tasks.Include(t => t.CreatedBy).FirstOrDefaultAsync(t => t.Id == id);
    }

    public async Task<Task> CreateTaskAsync(Task task, int userId)
    {
        task.CreatedById = userId;
        _context.Tasks.Add(task);
        await _context.SaveChangesAsync();
        return task;
    }

    public async Task<Task?> UpdateTaskAsync(Task task, int userId)
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
            ChangeDescription = "Task updated",
            ChangedById = userId
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

    internal async System.Threading.Tasks.Task UpdateTaskAsync(Guid id, Task updatedTask)
    {
        throw new NotImplementedException();
    }

    internal async System.Threading.Tasks.Task CreateTaskAsync(Task newTask)
    {
        throw new NotImplementedException();
    }
}
