using System;
using System.Collections.Generic;

namespace TaskManagerBackend.Models
{
    public class User
    {
        public int Id { get; set; } = 0;
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // Relação 1:N com as tarefas
        public List<Task> Tasks { get; set; } = new List<Task>();
    }
}
