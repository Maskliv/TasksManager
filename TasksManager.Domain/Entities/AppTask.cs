using System.ComponentModel.DataAnnotations.Schema;

namespace TasksManager.Domain.Entities
{
    public class AppTask : EntityBase
    {
        public required string Title { get; set; }
        public required string Description { get; set; }
        public required DateTime DeadlineDate { get; set; }
        public bool IsCompleted { get; set; }

        [ForeignKey("UserCreated")]
        public required int IdUserCreated { get; set; }
        public required User UserCreated { get; set; }

        [ForeignKey("UserAssigned")]
        public int? IdUserAssigned { get; set;}
        public User? UserAssigned { get; set; }

    }
}
