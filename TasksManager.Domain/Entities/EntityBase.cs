using System.ComponentModel.DataAnnotations;

namespace TasksManager.Domain.Entities
{
    public class EntityBase
    {
        [Key]
        public int Id { get; set; }
        public required DateTime DateCreated { get; set; }
        public DateTime? DateUpdated { get; set; }
    }
}
