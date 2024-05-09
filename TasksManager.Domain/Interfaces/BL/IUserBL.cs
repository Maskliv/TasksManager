using TasksManager.Domain.DTO;
using TasksManager.Domain.Entities;

namespace TasksManager.Domain.Interfaces.BL
{
    public interface IUserBL
    {
        Task<User?> GetByIdAsync(int id);
        Task<User?> GetByUsernameAsync(string userName);
        Task<List<User>?> GetAllAsync();
        Task<User> CreateAsync(UserDto newUser);
        Task<User> UpdateAsync(UserDto user);
        Task<bool> ChangePassword(ChangePwdDto dataPwds);
        Task<bool> DeleteByIdAsync(int id);
    }
}
