using AutoMapper;
using Moq;
using Xunit;
using TBCTest.Data;
using TBCTest.Managers;
using TBCTest.Models;
using TBCTest.Models.DTOs;
using TBCTest.Repositories;
using TBCTest.Services;
using Assert = Xunit.Assert;

namespace TBCTest.Tests
{
    public class PersonManagerTests
    {
        private readonly Mock<IPersonRepository> _repoMock;
        private readonly Mock<IUnitOfWork> _uowMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly Mock<IDbLocalizationService> _localizerMock;
        private readonly PersonManager _manager;

        public PersonManagerTests()
        {
            _repoMock = new Mock<IPersonRepository>();
            _uowMock = new Mock<IUnitOfWork>();
            _mapperMock = new Mock<IMapper>();
            _localizerMock = new Mock<IDbLocalizationService>();

            _manager = new PersonManager(
                _repoMock.Object,
                _uowMock.Object,
                _mapperMock.Object,
                _localizerMock.Object
            );
        }

        [Fact]
        public async Task CreateAsync_Should_AddPerson_And_Commit_And_ReturnDto()
        {
            var createDto = new CreatePersonDto
            {
                FirstNameEn = "Test",
                FirstNameGe = "ტესტი",
                LastNameEn = "User",
                LastNameGe = "პირველი",
                Gender = "Male",
                PersonalNumber = "12345678901",
                BirthDate = DateTime.Today.AddYears(-30),
                CityId = 1,
                PhoneNumbers = new List<PhoneNumberDto>
                {
                    new PhoneNumberDto { Type = "Mobile", Number = "599123456" }
                }
            };

            var entity = new Person { Id = 42 };
            var dto = new PersonDto { Id = 42 };

            _mapperMock.Setup(m => m.Map<Person>(createDto)).Returns(entity);
            _mapperMock.Setup(m => m.Map<PersonDto>(entity)).Returns(dto);

            var result = await _manager.CreateAsync(createDto);

            _repoMock.Verify(r => r.AddAsync(entity), Times.Once);
            _uowMock.Verify(u => u.CommitAsync(), Times.Once);
            Assert.Equal(42, result.Id);
            Assert.Same(dto, result);
        }

        [Fact]
        public async Task UpdateAsync_WhenNotFound_ReturnsFalse()
        {
            _repoMock.Setup(r => r.GetByIdAsync(It.IsAny<int>()))
                     .ReturnsAsync((Person?)null);

            var result = await _manager.UpdateAsync(123, new CreatePersonDto());

            Assert.False(result);
            _repoMock.Verify(r => r.UpdateAsync(It.IsAny<Person>()), Times.Never);
            _uowMock.Verify(u => u.CommitAsync(), Times.Never);
        }

        [Fact]
        public async Task UpdateAsync_WhenFound_UpdatesAndCommits_ReturnsTrue()
        {
            var existing = new Person { Id = 5 };
            _repoMock.Setup(r => r.GetByIdAsync(5)).ReturnsAsync(existing);

            var dto = new CreatePersonDto { FirstNameEn = "X" };
            var result = await _manager.UpdateAsync(5, dto);

            Assert.True(result);
            _repoMock.Verify(r => r.UpdateAsync(existing), Times.Once);
            _uowMock.Verify(u => u.CommitAsync(), Times.Once);
        }

        [Fact]
        public async Task DeleteAsync_WhenNotFound_ReturnsFalse()
        {
            _repoMock.Setup(r => r.GetByIdAsync(It.IsAny<int>()))
                     .ReturnsAsync((Person?)null);

            var result = await _manager.DeleteAsync(99);

            Assert.False(result);
            _uowMock.Verify(u => u.CommitAsync(), Times.Never);
        }

        [Fact]
        public async Task DeleteAsync_WhenFound_DeletesAndCommits_ReturnsTrue()
        {
            var existing = new Person { Id = 7 };
            _repoMock.Setup(r => r.GetByIdAsync(7)).ReturnsAsync(existing);

            var result = await _manager.DeleteAsync(7);

            Assert.True(result);
            _repoMock.Verify(r => r.DeleteAsync(existing), Times.Once);
            _uowMock.Verify(u => u.CommitAsync(), Times.Once);
        }

        [Fact]
        public async Task AddRelationAsync_SelfRelation_ReturnsFalse()
        {
            var dto = new CreateRelationDto { PersonId = 1, RelatedPersonId = 1 };
            _localizerMock.Setup(l => l.Get(It.IsAny<string>()))
                          .Returns("Cannot relate to self.");

            var (ok, msg) = await _manager.AddRelationAsync(dto);

            Assert.False(ok);
            Assert.Equal("Cannot relate to self.", msg);
            _uowMock.Verify(u => u.CommitAsync(), Times.Never);
        }

        [Fact]
        public async Task AddRelationAsync_MissingPerson_ReturnsFalse()
        {
            _repoMock.Setup(r => r.ExistsAsync(1)).ReturnsAsync(false);
            var dto = new CreateRelationDto { PersonId = 1, RelatedPersonId = 2 };

            _localizerMock.Setup(l => l.Get("PersonNotFound"))
                          .Returns("Person not found.");

            var (ok, msg) = await _manager.AddRelationAsync(dto);

            Assert.False(ok);
            Assert.Equal("Person not found.", msg);
        }

        [Fact]
        public async Task AddRelationAsync_Success_ReturnsTrueAndMessage()
        {
            _repoMock.Setup(r => r.ExistsAsync(It.IsAny<int>()))
                     .ReturnsAsync(true);
            _localizerMock.Setup(l => l.Get("RelationAdded"))
                          .Returns("Relation added.");

            var dto = new CreateRelationDto { PersonId = 1, RelatedPersonId = 2, RelationType = "Colleague" };
            var (ok, msg) = await _manager.AddRelationAsync(dto);

            Assert.True(ok);
            Assert.Equal("Relation added.", msg);
            _repoMock.Verify(r => r.AddRelationAsync(It.IsAny<PersonRelation>()), Times.Once);
            _uowMock.Verify(u => u.CommitAsync(), Times.Once);
        }

        [Fact]
        public async Task RemoveRelationAsync_Missing_ReturnsFalse()
        {
            _repoMock.Setup(r => r.ExistsAsync(It.IsAny<int>()))
                     .ReturnsAsync(false);

            _localizerMock.Setup(l => l.Get("PersonOrRelatedNotFound"))
                          .Returns("Not found.");

            var (ok, msg) = await _manager.RemoveRelationAsync(1, 2);

            Assert.False(ok);
            Assert.Equal("Not found.", msg);
        }

        [Fact]
        public async Task RemoveRelationAsync_Success_ReturnsTrue()
        {
            _repoMock.Setup(r => r.ExistsAsync(It.IsAny<int>()))
                     .ReturnsAsync(true);

            _localizerMock.Setup(l => l.Get("RelationRemoved"))
                          .Returns("Removed.");

            var (ok, msg) = await _manager.RemoveRelationAsync(1, 2);

            Assert.True(ok);
            Assert.Equal("Removed.", msg);
            _repoMock.Verify(r => r.RemoveRelationAsync(1, 2), Times.Once);
            _uowMock.Verify(u => u.CommitAsync(), Times.Once);
        }

        [Fact]
        public async Task SearchAsync_MapsDtos_And_ReturnsTotal()
        {
            var persons = new List<Person>
            {
                new Person { Id = 1 },
                new Person { Id = 2 }
            };
            _repoMock.Setup(r => r.SearchAsync(It.IsAny<PersonSearchParams>()))
                     .ReturnsAsync((persons, 2));

            _mapperMock.Setup(m => m.Map<List<PersonDto>>(persons))
                       .Returns(new List<PersonDto>
                       {
                           new() { Id = 1 },
                           new() { Id = 2 }
                       });

            var (dtos, total) = await _manager.SearchAsync(new PersonSearchParams { PageNumber = 1, PageSize = 10 });

            Assert.Equal(2, total);
            Assert.Collection(dtos,
                d => Assert.Equal(1, d.Id),
                d => Assert.Equal(2, d.Id));
        }
    }
}
