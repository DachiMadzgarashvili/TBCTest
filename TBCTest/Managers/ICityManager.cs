using TBCTest.Models.DTOs;

namespace TBCTest.Managers
{
    public interface ICityManager
    {
        Task<List<CityDto>> GetAllAsync();
        Task<CityDto?> GetByIdAsync(int id);
        Task<CityDto> CreateAsync(CreateCityDto dto);
        Task<(bool Success, string Message)> UpdateAsync(int id, CreateCityDto dto);
        Task<(bool Success, string Message)> DeleteAsync(int id);
    }
}
