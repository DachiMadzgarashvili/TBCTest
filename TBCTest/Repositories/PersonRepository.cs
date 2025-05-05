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
        public PersonRepository(AppDbContext context) => _context = context;

        public async Task<List<Person>> GetAllAsync()
            => await _context.People
                .Include(p => p.City)
                .Include(p => p.PhoneNumbers)
                .Include(p => p.RelatedPeople)
                    .ThenInclude(r => r.RelatedPerson)
                .ToListAsync();

        public async Task<Person?> GetByIdAsync(int id)
            => await _context.People
                .Include(p => p.City)
                .Include(p => p.PhoneNumbers)
                .Include(p => p.RelatedPeople)
                    .ThenInclude(r => r.RelatedPerson)
                .FirstOrDefaultAsync(p => p.Id == id);

        public Task AddAsync(Person person)
        {
            _context.People.Add(person);
            return Task.CompletedTask;
        }

        public Task UpdateAsync(Person person)
        {
            _context.People.Update(person);
            return Task.CompletedTask;
        }

        public Task DeleteAsync(Person person)
        {
            _context.People.Remove(person);
            return Task.CompletedTask;
        }

        public async Task<bool> ExistsAsync(int id)
            => await _context.People.AnyAsync(p => p.Id == id);

        public Task AddRelationAsync(PersonRelation relation)
        {
            _context.PersonRelations.Add(relation);
            return Task.CompletedTask;
        }

        public async Task RemoveRelationAsync(int personId, int relatedPersonId)
        {
            var relation = await _context.PersonRelations
                .FirstOrDefaultAsync(r => r.PersonId == personId && r.RelatedPersonId == relatedPersonId);
            if (relation != null)
                _context.PersonRelations.Remove(relation);
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
                FullName = $"{p.FirstNameEn} {p.LastNameEn}",
                RelationCounts = p.RelatedPeople
                    .GroupBy(r => r.RelationType)
                    .ToDictionary(g => g.Key, g => g.Count())
            }).ToList();
        }
        public async Task<(List<Person> Items, int TotalCount)> SearchAsync(PersonSearchParams p)
        {
            var query = _context.People
                .Include(x => x.City)
                .Include(x => x.PhoneNumbers)
                .Include(x => x.RelatedPeople).ThenInclude(r => r.RelatedPerson)
                .AsQueryable();

            // Quick search
            if (!string.IsNullOrWhiteSpace(p.Quick))
            {
                query = query.Where(x =>
                    EF.Functions.Like(x.FirstNameGe, $"%{p.Quick}%") ||
                    EF.Functions.Like(x.FirstNameEn, $"%{p.Quick}%") ||
                    EF.Functions.Like(x.LastNameGe, $"%{p.Quick}%") ||
                    EF.Functions.Like(x.LastNameEn, $"%{p.Quick}%") ||
                    EF.Functions.Like(x.PersonalNumber, $"%{p.Quick}%"));
            }

            // Detailed filters
            if (!string.IsNullOrWhiteSpace(p.FirstNameGe))
                query = query.Where(x => EF.Functions.Like(x.FirstNameGe, $"%{p.FirstNameGe}%"));
            if (!string.IsNullOrWhiteSpace(p.FirstNameEn))
                query = query.Where(x => EF.Functions.Like(x.FirstNameEn, $"%{p.FirstNameEn}%"));
            if (!string.IsNullOrWhiteSpace(p.LastNameGe))
                query = query.Where(x => EF.Functions.Like(x.LastNameGe, $"%{p.LastNameGe}%"));
            if (!string.IsNullOrWhiteSpace(p.LastNameEn))
                query = query.Where(x => EF.Functions.Like(x.LastNameEn, $"%{p.LastNameEn}%"));
            if (!string.IsNullOrWhiteSpace(p.Gender))
                query = query.Where(x => x.Gender == p.Gender);
            if (!string.IsNullOrWhiteSpace(p.PersonalNumber))
                query = query.Where(x => EF.Functions.Like(x.PersonalNumber, $"%{p.PersonalNumber}%"));
            if (p.CityId.HasValue)
                query = query.Where(x => x.CityId == p.CityId.Value);

            var total = await query.CountAsync();
            var items = await query
                .Skip((p.PageNumber - 1) * p.PageSize)
                .Take(p.PageSize)
                .ToListAsync();

            return (items, total);
        }
    }
}
