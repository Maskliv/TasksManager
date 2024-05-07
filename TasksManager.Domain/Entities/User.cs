using Microsoft.EntityFrameworkCore;

namespace TasksManager.Domain.Entities
{
    [Index(nameof(Username), IsUnique = true)]
    public class User : EntityBase
    {
        
        public required string Username { get; set; }
        public required string Role {  get; set; }
        public required string Password { get; set; }
        public required string Email { get; set; }
        public required string Name { get; set; }
        public string? LastName { get; set; }
        public string? Phone { get; set; }

        public ICollection<AppTask> CreatedTask { get; set; } = new List<AppTask>();
        public ICollection<AppTask> AssignedTask { get; set; } = new List<AppTask>();


    }
}
