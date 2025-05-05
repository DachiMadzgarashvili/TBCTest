using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using TBCTest.Models;
using TBCTest.Models.DTOs;
using TBCTest.Repositories;

namespace TBCTest.Managers
{
    public class PersonManager : IPersonManager
    {
        private readonly IPersonRepository _repo;
        private readonly IMapper _mapper;

        public PersonManager(IPersonRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<List<PersonDto>> GetAllAsync()
        {
            var people = await _repo.GetAllAsync();
            return _mapper.Map<List<PersonDto>>(people);
        }

        public async Task<PersonDto?> GetByIdAsync(int id)
        {
            var person = await _repo.GetByIdAsync(id);
            return person == null ? null : _mapper.Map<PersonDto>(person);
        }

        public async Task<PersonDto> CreateAsync(CreatePersonDto dto)
        {
            var person = _mapper.Map<Person>(dto);
            await _repo.AddAsync(person);
            return _mapper.Map<PersonDto>(person);
        }

        public async Task<bool> UpdateAsync(int id, CreatePersonDto dto)
        {
            if (!await _repo.ExistsAsync(id))
                return false;

            var existing = await _repo.GetByIdAsync(id);
            if (existing == null)
                return false;

            _mapper.Map(dto, existing);

            await _repo.UpdateAsync(existing);
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var person = await _repo.GetByIdAsync(id);
            if (person == null)
                return false;

            await _repo.DeleteAsync(person);
            return true;
        }
    }
}
