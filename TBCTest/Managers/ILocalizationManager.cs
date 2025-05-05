using System.Collections.Generic;
using System.Threading.Tasks;
using TBCTest.Models;

namespace TBCTest.Managers
{
    public interface ILocalizationManager
    {
        Task<List<Localization>> GetAllAsync();
        Task<List<Localization>> GetByKeyAsync(string key);
        Task<(bool Success, string Message)> UpdateAsync(int id, Localization updated);
        Task<List<Localization>> SearchAsync(string? key, string? language);
    }
}
