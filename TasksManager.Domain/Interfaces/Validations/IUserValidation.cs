using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TasksManager.Domain.DTO;

namespace TasksManager.Domain.Interfaces.Validations
{
    public interface IUserValidation
    {
        bool IsPasswordCorrect(int idUser, string pwd);
        void ValidateNewPasswordFormat(string newPwd);
        void ValidateEmail(ref UserDto user);
        void ValidateUserCreate(ref UserDto user);
        void ValidateUserUpdate(UserDto user);

    }
}
