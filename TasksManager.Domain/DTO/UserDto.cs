﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TasksManager.Domain.DTO
{
    public class UserDto: BaseDto
    {
        public required string username { get; set; }
        public string? password { get; set; }
        public required string email { get; set; }
        public required string name { get; set; }
        public string? lastName { get; set; }
        public string? phone { get; set; }
    }
}
