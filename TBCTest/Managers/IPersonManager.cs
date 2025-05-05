using TBCTest.Models.DTOs;

namespace TBCTest.Managers
{
    public interface IPersonManager
    {
        Task<List<PersonDto>> GetAllAsync();
        Task<PersonDto?> GetByIdAsync(int id);
        Task<PersonDto> CreateAsync(CreatePersonDto dto);
        Task<bool> UpdateAsync(int id, CreatePersonDto dto);
        Task<bool> DeleteAsync(int id);
        Task<(bool Success, string Message)> AddRelationAsync(CreateRelationDto dto);
        Task<(bool Success, string Message)> RemoveRelationAsync(int personId, int relatedPersonId);
        Task<List<PersonRelationReportDto>> GetRelationReportAsync();
        Task<(bool Success, string? Message, string? NewImagePath)> UploadImageAsync(int id, IFormFile file);
        Task<(bool Success, string? Message)> RemoveImageAsync(int id);
    }
}