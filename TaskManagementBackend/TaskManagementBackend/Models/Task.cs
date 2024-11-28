public class Task
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public bool IsUrgent { get; set; }
    public string Status { get; set; } = "Pending"; // Valores: Pending, InProgress, Completed, Canceled
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public int CreatedById { get; set; }
    public User CreatedBy { get; set; } = null!;
}
