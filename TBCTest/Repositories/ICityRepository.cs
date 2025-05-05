using System.Collections.Generic;
using System.Threading.Tasks;
using TBCTest.Models;

namespace TBCTest.Repositories
{
    public interface ICityRepository
    {
        Task<List<City>> GetAllAsync();
        Task<City?> GetByIdAsync(int id);
        Task AddAsync(City city);
        Task UpdateAsync(City city);
        Task DeleteAsync(City city);
        Task<bool> ExistsAsync(int id);
    }
}
