using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TBCTest.Data;
using TBCTest.Models;

namespace TBCTest.Repositories
{
    public class LocalizationRepository : ILocalizationRepository
    {
        private readonly AppDbContext _context;

        public LocalizationRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Localization>> GetAllAsync()
        {
            return await _context.Localizations
                .OrderBy(l => l.Key)
                .ThenBy(l => l.Language)
                .ToListAsync();
        }

        public async Task<List<Localization>> GetByKeyAsync(string key)
        {
            return await _context.Localizations
                .Where(l => l.Key == key)
                .ToListAsync();
        }

        public async Task<Localization?> GetByIdAsync(int id)
        {
            return await _context.Localizations.FindAsync(id);
        }

        public async Task UpdateAsync(Localization localization)
        {
            _context.Localizations.Update(localization);
            await _context.SaveChangesAsync();
        }
        public async Task<List<Localization>> SearchAsync(string? key, string? language)
        {
            var query = _context.Localizations.AsQueryable();

            if (!string.IsNullOrWhiteSpace(key))
                query = query.Where(l => l.Key.Contains(key));

            if (!string.IsNullOrWhiteSpace(language))
                query = query.Where(l => l.Language == language);

            return await query
                .OrderBy(l => l.Key)
                .ThenBy(l => l.Language)
                .ToListAsync();
        }
    }
}
