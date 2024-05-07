using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TasksManager.Domain.DTO;

namespace TasksManager.Domain.Interfaces.BL
{
    public interface IAuthBL
    {
        Task<string?> Login(LoginDto loginData);
    }
}
