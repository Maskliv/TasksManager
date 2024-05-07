using TasksManager.Domain.DTO;
using TasksManager.Domain.Entities;
using TasksManager.Domain.Exceptions;
using TasksManager.Domain.Interfaces.BL;
using TasksManager.Domain.Interfaces.Persistence;
using TasksManager.Domain.Interfaces.Validations;

namespace TasksManager.Application.BL
{
    public class UserBL : IUserBL
    {
        private readonly IGenericRepository<User> _userRepo;
        private readonly IUserValidation _userValidation;

        public UserBL(IGenericRepository<User> userRepository, IUserValidation userValidation)
        {
            _userRepo = userRepository;
            _userValidation = userValidation;
        }

        public Task<bool> ChangePassword(ChangePwdDto dataPwds)
        {
            throw new NotImplementedException();
        }

        public Task<User?> CreateAsync(UserDto newUser)
        {
            _userValidation.ValidateRequiredFieldsUser(newUser);
            _userValidation.ValidateEmail(newUser.email);
            _userValidation.ValidatePasswordFormat(newUser.password ?? throw new ClientException("No se proporcionó contraseña"));


        }

        public Task<bool> DeleteByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<User>?> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<User?> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<User?> GetByUsernameAsync(string userName)
        {
            throw new NotImplementedException();
        }

        public Task<User?> UpdateAsync(UserDto user)
        {
            throw new NotImplementedException();
        }
    }
}
