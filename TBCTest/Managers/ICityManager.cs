using System.Collections.Generic;
using System.Threading.Tasks;
using TBCTest.Models.DTOs;

namespace TBCTest.Managers
{
    public interface ICityManager
    {
        Task<List<CityDto>> GetAllAsync();
        Task<CityDto?> GetByIdAsync(int id);
        Task<CityDto> CreateAsync(CreateCityDto dto);
        Task<bool> UpdateAsync(int id, CreateCityDto dto);
        Task<bool> DeleteAsync(int id);
    }
}
