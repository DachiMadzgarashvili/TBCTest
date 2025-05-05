using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TBCTest.Data;
using TBCTest.Models;
using TBCTest.Models.DTOs;

namespace TBCTest.Repositories
{
    public class PersonRepository : IPersonRepository
    {
        private readonly AppDbContext _context;

        public PersonRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Person>> GetAllAsync()
        {
            return await _context.People
                .Include(p => p.City)
                .Include(p => p.PhoneNumbers)
                .Include(p => p.RelatedPeople)
                    .ThenInclude(r => r.RelatedPerson)
                .ToListAsync();
        }

        public async Task<Person?> GetByIdAsync(int id)
        {
            return await _context.People
                .Include(p => p.City)
                .Include(p => p.PhoneNumbers)
                .Include(p => p.RelatedPeople)
                    .ThenInclude(r => r.RelatedPerson)
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task AddAsync(Person person)
        {
            _context.People.Add(person);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Person person)
        {
            _context.People.Update(person);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Person person)
        {
            _context.People.Remove(person);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> ExistsAsync(int id)
        {
            return await _context.People.AnyAsync(p => p.Id == id);
        }
        public async Task AddRelationAsync(PersonRelation relation)
        {
            _context.PersonRelations.Add(relation);
            await _context.SaveChangesAsync();
        }

        public async Task RemoveRelationAsync(int personId, int relatedPersonId)
        {
            var relation = await _context.PersonRelations
                .FirstOrDefaultAsync(r => r.PersonId == personId && r.RelatedPersonId == relatedPersonId);

            if (relation != null)
            {
                _context.PersonRelations.Remove(relation);
                await _context.SaveChangesAsync();
            }
        }
        public async Task<List<PersonRelationReportDto>> GetRelationReportAsync()
        {
            var people = await _context.People
                .Include(p => p.RelatedPeople)
                .ThenInclude(r => r.RelatedPerson)
                .ToListAsync();

            return people.Select(p => new PersonRelationReportDto
            {
                PersonId = p.Id,
                FullName = p.FirstNameEn + " " + p.LastNameEn,
                RelationCounts = p.RelatedPeople
                    .GroupBy(r => r.RelationType)
                    .ToDictionary(g => g.Key, g => g.Count())
            }).ToList();
        }
    }
}
