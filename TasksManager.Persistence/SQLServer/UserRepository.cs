using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TasksManager.Domain.Entities;
using TasksManager.Domain.Interfaces.Persistence;

namespace TasksManager.Persistence.SQLServer
{
    internal class UserRepository : IGenericRepository<User>
    {
        private readonly ApplicationDbContext _context;

        public UserRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<User?> GetByIdAsync(int id)
        {
            return await _context.Users.FindAsync(id);
        }

        public async Task<List<User>> GetAllAsync()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task<User> CreateAsync(User newUser)
        {
            _context.Users.Add(newUser);
            await _context.SaveChangesAsync();
            return newUser;
        }

        public async Task<User?> UpdateAsync(User user)
        {
            var existingUser = await _context.Users.FindAsync(user.Id);
            if (existingUser == null) return null;

            existingUser.Username = user.Username;
            existingUser.Role = user.Role;
            existingUser.Password = user.Password;
            existingUser.Email = user.Email;
            existingUser.Name = user.Name;
            existingUser.LastName = user.LastName;
            existingUser.Phone = user.Phone;
            existingUser.DateUpdated = DateTime.Now;

            await _context.SaveChangesAsync();
            return existingUser;
        }

        public async Task<bool> DeleteByIdAsync(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null) return false;

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
            return true;
        }

    }
}
