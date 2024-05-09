using AutoMapper;
using TasksManager.Domain.DTO;
using TasksManager.Domain.Entities;
using TasksManager.Domain.Enum;
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
        private readonly IMapper _mapper;

        public UserBL(IGenericRepository<User> userRepository, IUserValidation userValidation, IMapper mapper)
        {
            _userRepo = userRepository;
            _userValidation = userValidation;
            _mapper = mapper;
        }

        public async Task<List<User>?> GetAllAsync()
        {
            return await _userRepo.GetAllAsync();
        }

        public async Task<User?> GetByIdAsync(int id)
        {
            return await _userRepo.GetByIdAsync(id);
        }

        public async Task<User?> GetByUsernameAsync(string userName)
        {
            var users = await _userRepo.GetAllAsync();
            return users?.FirstOrDefault(u => u.Username == userName);
        }

        public async Task<User> CreateAsync(UserDto newUser)
        {
            _userValidation.ValidateRequiredFieldsUser(newUser);
            _userValidation.ValidateEmail(newUser.email);
            _userValidation.ValidatePasswordFormat(newUser.password ?? throw new ClientException("No se proporcionó contraseña"));
            
            var userToCreate = _mapper.Map<User>(newUser);

            _userValidation.ValidateUniqueFields(userToCreate, await _userRepo.GetAllAsync());

            return await _userRepo.CreateAsync(userToCreate);
        }

        public async Task<User> UpdateAsync(UserDto user)
        {
            var userPwd = (await _userRepo.GetByIdAsync(user.id))?.Password;
            if (userPwd == null)
                throw new ClientException("El usuario no existe");
            
            user.password = userPwd;

            _userValidation.ValidateRequiredFieldsUser(user);
            _userValidation.ValidateEmail(user.email);

            var userToUpdate = _mapper.Map<User>(user);

            _userValidation.ValidateUniqueFields(userToUpdate, await _userRepo.GetAllAsync());

            return await _userRepo.UpdateAsync(userToUpdate)?? throw new ClientException("El usuario no existe");

        }

        public async Task<bool> ChangePassword(ChangePwdDto dataPwds)
        {
            var userToUpdate = await _userRepo.GetByIdAsync(dataPwds.idUser);

            if (userToUpdate == null)
                throw new ClientException("El usuario no existe");
            
            if (!_userValidation.IsPasswordCorrect(dataPwds.oldPwd, userToUpdate.Password))
            {
                throw new ClientException("La contraseña actual no es correcta");
            }

            _userValidation.ValidatePasswordFormat(dataPwds.newPwd);

            userToUpdate.Password = dataPwds.newPwd;
            
            var _ = await _userRepo.UpdateAsync(userToUpdate) ?? throw new ClientException("El usuario no existe");

            return true;
        }

        public async Task<bool> DeleteByIdAsync(int id)
        {
            var user = await _userRepo.GetByIdAsync(id);
            if (user == null) 
                throw new ClientException("El usuario no existe");

            return await _userRepo.DeleteByIdAsync(id);
        }
    }
}
