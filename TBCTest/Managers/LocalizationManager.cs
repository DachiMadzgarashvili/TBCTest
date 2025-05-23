using System.Collections.Generic;
using System.Threading.Tasks;
using TBCTest.Data;
using TBCTest.LocalizationSupport;
using TBCTest.Models;
using TBCTest.Repositories;
using TBCTest.Services;

namespace TBCTest.Managers
{
    public class LocalizationManager : ILocalizationManager
    {
        private readonly ILocalizationRepository _repo;
        private readonly IUnitOfWork _uow;
        private readonly IDbLocalizationService _localizer;

        public LocalizationManager(
            ILocalizationRepository repo,
            IUnitOfWork uow,
            IDbLocalizationService localizer)
        {
            _repo = repo;
            _uow = uow;
            _localizer = localizer;
        }

        public Task<List<Localization>> GetAllAsync() => _repo.GetAllAsync();

        public Task<List<Localization>> GetByKeyAsync(string key)
            => _repo.GetByKeyAsync(key);

        public async Task<(bool Success, string Message)> UpdateAsync(int id, Localization updated)
        {
            var existing = await _repo.GetByIdAsync(id);
            if (existing == null)
                return (false, _localizer.Get(AppMessages.LocalizationNotFound));

            existing.Key = updated.Key;
            existing.Value = updated.Value;
            existing.Language = updated.Language;

            await _repo.UpdateAsync(existing);
            await _uow.CommitAsync();
            return (true, _localizer.Get(AppMessages.LocalizationUpdated));
        }

        public Task<List<Localization>> SearchAsync(string? key, string? language)
            => _repo.SearchAsync(key, language);
    }
}
