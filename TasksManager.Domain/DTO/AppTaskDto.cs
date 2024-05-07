using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TasksManager.Domain.DTO
{
    public class AppTaskDto: BaseDto
    {
        public required string title { get; set; }
        public required string description { get; set; }
        public required DateTime deadlineDate { get; set; }
        public bool isCompleted { get; set; }
    }
}
