using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TBCTest.Data;
using TBCTest.Models;

namespace TBCTest.Repositories
{
    public class CityRepository : ICityRepository
    {
        private readonly AppDbContext _context;
        public CityRepository(AppDbContext context) => _context = context;

        public async Task<List<City>> GetAllAsync()
            => await _context.Cities.ToListAsync();

        public async Task<City?> GetByIdAsync(int id)
            => await _context.Cities.FindAsync(id);

        public Task AddAsync(City city)
        {
            _context.Cities.Add(city);
            return Task.CompletedTask;
        }

        public Task UpdateAsync(City city)
        {
            _context.Cities.Update(city);
            return Task.CompletedTask;
        }

        public Task DeleteAsync(City city)
        {
            _context.Cities.Remove(city);
            return Task.CompletedTask;
        }

        public async Task<bool> ExistsAsync(int id)
            => await _context.Cities.AnyAsync(c => c.Id == id);
    }
}
