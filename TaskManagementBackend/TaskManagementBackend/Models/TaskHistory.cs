using System;

namespace TaskManagementAPI.Models
{
    public class TaskHistory
    {
        // Trocado os Ids do tipo Guid para int, vejam a diferença e escolham o melhor pra vocês,
        // por que tem métodos que estavam passando um valor do tipo Guid para um vaor inteiro,
        // como são tipos diferentes ai acaba dando erro 
        public int Id { get; set; }
        public int TaskId { get; set; } // ID da tarefa associada
        public string Action { get; set; } // Ação realizada (Ex: "Criada", "Atualizada")
        public string Description { get; set; } // Descrição da ação
        public DateTime CreatedAt { get; set; } // Data da ação

        public virtual TaskItem Task { get; set; } // Relacionamento com a tarefa
        public object ChangedBy { get; internal set; }
    }
}