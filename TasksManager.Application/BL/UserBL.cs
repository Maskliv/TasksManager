using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TasksManager.Domain.DTO;
using TasksManager.Domain.Entities;
using TasksManager.Domain.Interfaces.BL;
using TasksManager.Domain.Interfaces.Persistence;

namespace TasksManager.Application.BL
{
    public class UserBL : IUserBL
    {
        private readonly IGenericRepository<User> _userRepo;

        public UserBL(IGenericRepository<User> userRepository)
        {
            _userRepo = userRepository;
        }

        public Task<bool> ChangePassword(ChangePwdDto dataPwds)
        {
            throw new NotImplementedException();
        }

        public Task<User?> CreateAsync(UserDto newUser)
        {
            throw new NotImplementedException(); 
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
