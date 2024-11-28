using System;

namespace TaskManagementAPI.Models
{
    public class TaskHistory
    {
        public Guid Id { get; set; } // ID único do histórico
        public Guid TaskId { get; set; } // ID da tarefa associada
        public string Action { get; set; } // Ação realizada (Ex: "Criada", "Atualizada")
        public string Description { get; set; } // Descrição da ação
        public DateTime CreatedAt { get; set; } // Data da ação

        public virtual Task Task { get; set; } // Relacionamento com a tarefa
        public object ChangedBy { get; internal set; }
    }
}
