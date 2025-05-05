using System.Collections.Generic;
using System.Threading.Tasks;
using TBCTest.Models;
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
        Task<bool> AddRelationAsync(CreateRelationDto dto);
        Task<bool> RemoveRelationAsync(int personId, int relatedPersonId);
        Task<Person?> GetEntityAsync(int id);
        Task UpdateImagePathAsync(Person person);
        Task<List<PersonRelationReportDto>> GetRelationReportAsync();

    }
}
