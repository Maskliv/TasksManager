using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TasksManager.Domain.DTO
{
    public class BaseDto
    {
        public int id { get; set; }
        public DateTime dateCreated { get; set; }
        public DateTime dateUpdated { get; set; }
    }
}
