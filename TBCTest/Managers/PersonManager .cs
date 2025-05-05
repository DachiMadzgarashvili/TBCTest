using AutoMapper;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using TBCTest.LocalizationSupport;
using TBCTest.Models;
using TBCTest.Models.DTOs;
using TBCTest.Repositories;
using TBCTest.Services;

namespace TBCTest.Managers
{
    public class PersonManager : IPersonManager
    {
        private readonly IPersonRepository _repo;
        private readonly IMapper _mapper;
        private readonly IDbLocalizationService _localizer;

        public PersonManager(
            IPersonRepository repo,
            IMapper mapper,
            IDbLocalizationService localizer)
        {
            _repo = repo;
            _mapper = mapper;
            _localizer = localizer;
        }

        public async Task<List<PersonDto>> GetAllAsync()
        {
            var people = await _repo.GetAllAsync();
            return _mapper.Map<List<PersonDto>>(people);
        }

        public async Task<PersonDto?> GetByIdAsync(int id)
        {
            var person = await _repo.GetByIdAsync(id);
            return person == null
                ? null
                : _mapper.Map<PersonDto>(person);
        }

        public async Task<PersonDto> CreateAsync(CreatePersonDto dto)
        {
            var person = _mapper.Map<Person>(dto);
            await _repo.AddAsync(person);
            return _mapper.Map<PersonDto>(person);
        }

        public async Task<bool> UpdateAsync(int id, CreatePersonDto dto)
        {
            var person = await _repo.GetByIdAsync(id);
            if (person == null) return false;

            _mapper.Map(dto, person);
            await _repo.UpdateAsync(person);
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var person = await _repo.GetByIdAsync(id);
            if (person == null) return false;

            await _repo.DeleteAsync(person);
            return true;
        }

        public async Task<(bool Success, string Message)> AddRelationAsync(CreateRelationDto dto)
        {
            if (dto.PersonId == dto.RelatedPersonId)
                return (false, _localizer.Get(AppMessages.CannotRelateToSelf));

            if (!await _repo.ExistsAsync(dto.PersonId))
                return (false, _localizer.Get(AppMessages.PersonNotFound));

            if (!await _repo.ExistsAsync(dto.RelatedPersonId))
                return (false, _localizer.Get(AppMessages.RelatedPersonNotFound));

            await _repo.AddRelationAsync(new PersonRelation
            {
                PersonId = dto.PersonId,
                RelatedPersonId = dto.RelatedPersonId,
                RelationType = dto.RelationType
            });

            return (true, _localizer.Get(AppMessages.RelationAdded));
        }

        public async Task<(bool Success, string Message)> RemoveRelationAsync(int personId, int relatedPersonId)
        {
            if (!await _repo.ExistsAsync(personId) || !await _repo.ExistsAsync(relatedPersonId))
                return (false, _localizer.Get(AppMessages.PersonOrRelatedNotFound));

            await _repo.RemoveRelationAsync(personId, relatedPersonId);
            return (true, _localizer.Get(AppMessages.RelationRemoved));
        }

        public Task<List<PersonRelationReportDto>> GetRelationReportAsync()
            => _repo.GetRelationReportAsync();

        public async Task<(bool Success, string? Message, string? NewImagePath)> UploadImageAsync(int id, IFormFile file)
        {
            if (file == null || file.Length == 0)
                return (false, _localizer.Get(AppMessages.NoFileProvided), null);

            var person = await _repo.GetByIdAsync(id);
            if (person == null)
                return (false, _localizer.Get(AppMessages.PersonNotFound), null);

            try
            {
                var uploads = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images");
                Directory.CreateDirectory(uploads);

                var ext = Path.GetExtension(file.FileName);
                var fileName = $"{Guid.NewGuid()}{ext}";
                var dest = Path.Combine(uploads, fileName);

                using var stream = new FileStream(dest, FileMode.Create, FileAccess.Write);
                await file.CopyToAsync(stream);

                if (!string.IsNullOrEmpty(person.ImagePath))
                {
                    var old = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", person.ImagePath.TrimStart('/'));
                    if (File.Exists(old)) File.Delete(old);
                }

                person.ImagePath = $"/images/{fileName}";
                await _repo.UpdateAsync(person);

                return (true, null, person.ImagePath);
            }
            catch
            {
                return (false, _localizer.Get(AppMessages.ImageUploadFailed), null);
            }
        }

        public async Task<(bool Success, string Message)> RemoveImageAsync(int id)
        {
            var person = await _repo.GetByIdAsync(id);
            if (person == null || string.IsNullOrEmpty(person.ImagePath))
                return (false, _localizer.Get(AppMessages.NoImageToDelete));

            var full = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", person.ImagePath.TrimStart('/'));
            if (File.Exists(full)) File.Delete(full);

            person.ImagePath = null;
            await _repo.UpdateAsync(person);

            return (true, _localizer.Get(AppMessages.ImageDeleted));
        }
    }
}
