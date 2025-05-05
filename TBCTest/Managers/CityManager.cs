using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using TBCTest.Models;
using TBCTest.Models.DTOs;
using TBCTest.Repositories;

namespace TBCTest.Managers
{
    public class CityManager : ICityManager
    {
        private readonly ICityRepository _repo;
        private readonly IMapper _mapper;

        public CityManager(ICityRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<List<CityDto>> GetAllAsync()
        {
            var cities = await _repo.GetAllAsync();
            return _mapper.Map<List<CityDto>>(cities);
        }

        public async Task<CityDto?> GetByIdAsync(int id)
        {
            var city = await _repo.GetByIdAsync(id);
            return city == null ? null : _mapper.Map<CityDto>(city);
        }

        public async Task<CityDto> CreateAsync(CreateCityDto dto)
        {
            var city = _mapper.Map<City>(dto);
            await _repo.AddAsync(city);
            return _mapper.Map<CityDto>(city);
        }

        public async Task<bool> UpdateAsync(int id, CreateCityDto dto)
        {
            var city = await _repo.GetByIdAsync(id);
            if (city == null) return false;

            _mapper.Map(dto, city);
            await _repo.UpdateAsync(city);
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var city = await _repo.GetByIdAsync(id);
            if (city == null) return false;

            await _repo.DeleteAsync(city);
            return true;
        }
    }
}
