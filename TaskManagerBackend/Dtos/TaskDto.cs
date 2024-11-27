namespace TaskManagerBackend.Dtos
{
    public class TaskDto
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Level { get; set; }
        public string Status { get; private set; }
        public int UserId { get; set; }
        public User User { get; set; } = null;
        // Construtor dentro da classe
        public TaskDto()
        {
            Title = string.Empty;
            Description = string.Empty;
            Level = string.Empty;
            Status = string.Empty;
        }
        public void SetStatus(string status)
        {
            Status = status;
        }
    }
}
