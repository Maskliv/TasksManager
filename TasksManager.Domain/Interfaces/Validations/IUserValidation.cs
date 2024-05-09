using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TasksManager.Domain.DTO;
using TasksManager.Domain.Entities;

namespace TasksManager.Domain.Interfaces.Validations
{
    public interface IUserValidation
    {
        bool IsPasswordCorrect(string pwdClient, string pwdHashed);
        void ValidatePasswordFormat(string newPwd);
        void ValidateEmail(string userEmail);
        void ValidateRequiredFieldsUser(UserDto user);
        void ValidateUserUpdate(UserDto user);

        void ValidateUniqueFields(User user, List<User> users);

    }
}
