using System;

namespace TaskManagerBackend.Models
{
    public class TaskItem
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Level { get; set; } // "Low", "Medium", "High"
        public string Status { get; set; } // "To Do", "In Progress", "Done"
        public int UserId { get; set; } // FK para User
        public User User { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public Task()
        {
            Title = string.Empty;
            Description = string.Empty;
            Level = string.Empty;
            Status = string.Empty;
        }
    }
}
