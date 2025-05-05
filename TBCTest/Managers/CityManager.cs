using AutoMapper;
using TBCTest.LocalizationSupport;
using TBCTest.Models;
using TBCTest.Models.DTOs;
using TBCTest.Repositories;
using TBCTest.Services;

namespace TBCTest.Managers
{
    public class CityManager : ICityManager
    {
        private readonly ICityRepository _repo;
        private readonly IMapper _mapper;
        private readonly IDbLocalizationService _localizer;

        public CityManager(
            ICityRepository repo,
            IMapper mapper,
            IDbLocalizationService localizer)
        {
            _repo = repo;
            _mapper = mapper;
            _localizer = localizer;
        }

        public async Task<List<CityDto>> GetAllAsync()
        {
            var cities = await _repo.GetAllAsync();
            return _mapper.Map<List<CityDto>>(cities);
        }

        public async Task<CityDto?> GetByIdAsync(int id)
        {
            var city = await _repo.GetByIdAsync(id);
            return city == null
                ? null
                : _mapper.Map<CityDto>(city);
        }

        public async Task<CityDto> CreateAsync(CreateCityDto dto)
        {
            var city = _mapper.Map<City>(dto);
            await _repo.AddAsync(city);
            return _mapper.Map<CityDto>(city);
        }

        public async Task<(bool Success, string Message)> UpdateAsync(int id, CreateCityDto dto)
        {
            var city = await _repo.GetByIdAsync(id);
            if (city == null)
                return (false, _localizer.Get(AppMessages.CityNotFound));

            _mapper.Map(dto, city);
            await _repo.UpdateAsync(city);
            return (true, _localizer.Get(AppMessages.CityUpdated));
        }

        public async Task<(bool Success, string Message)> DeleteAsync(int id)
        {
            var city = await _repo.GetByIdAsync(id);
            if (city == null)
                return (false, _localizer.Get(AppMessages.CityNotFound));

            await _repo.DeleteAsync(city);
            return (true, _localizer.Get(AppMessages.CityDeleted));
        }
    }
}
