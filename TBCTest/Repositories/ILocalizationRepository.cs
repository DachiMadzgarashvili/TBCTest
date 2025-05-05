using TBCTest.Models;

namespace TBCTest.Repositories
{
    public interface ILocalizationRepository
    {
        Task<List<Localization>> GetAllAsync();
        Task<List<Localization>> GetByKeyAsync(string key);
        Task<Localization?> GetByIdAsync(int id);
        Task UpdateAsync(Localization localization);
        Task<List<Localization>> SearchAsync(string? key, string? language);
    }
}
