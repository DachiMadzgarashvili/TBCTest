using System.Collections.Generic;
using System.Threading.Tasks;
using TBCTest.Models;
using TBCTest.Models.DTOs;

namespace TBCTest.Repositories
{
    public interface IPersonRepository
    {
        Task<List<Person>> GetAllAsync();
        Task<Person?> GetByIdAsync(int id);
        Task AddAsync(Person person);
        Task UpdateAsync(Person person);
        Task DeleteAsync(Person person);
        Task<bool> ExistsAsync(int id); 
        Task AddRelationAsync(PersonRelation relation);
        Task RemoveRelationAsync(int personId, int relatedPersonId);
        Task<List<PersonRelationReportDto>> GetRelationReportAsync();

    }
}
