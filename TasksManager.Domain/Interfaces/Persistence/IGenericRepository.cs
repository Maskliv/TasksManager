using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TasksManager.Domain.Interfaces.Persistence
{
    public interface IGenericRepository<T>
    {
        Task<List<T>> GetAllAsync();
        Task<T?> GetByIdAsync(int id);
        Task<T> CreateAsync(T newEntry);
        Task<T?> UpdateAsync(T entry);
        Task<bool> DeleteByIdAsync(int id);
    }
}
