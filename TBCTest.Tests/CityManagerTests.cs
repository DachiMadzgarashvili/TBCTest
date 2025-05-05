
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
    public class CityManagerTests
    {
        private readonly Mock<ICityRepository> _repoMock;
        private readonly Mock<IUnitOfWork> _uowMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly Mock<IDbLocalizationService> _localizerMock;
        private readonly CityManager _manager;

        public CityManagerTests()
        {
            _repoMock = new Mock<ICityRepository>();
            _uowMock = new Mock<IUnitOfWork>();
            _mapperMock = new Mock<IMapper>();
            _localizerMock = new Mock<IDbLocalizationService>();

            _manager = new CityManager(
                _repoMock.Object,
                _uowMock.Object,
                _mapperMock.Object,
                _localizerMock.Object
            );
        }

        [Fact]
        public async Task CreateAsync_Should_AddCity_And_Commit_And_ReturnDto()
        {
            var createDto = new CreateCityDto { NameGe = "ქალაქი", NameEn = "City" };
            var entity = new City { Id = 100 };
            var dto = new CityDto { Id = 100 };

            _mapperMock.Setup(m => m.Map<City>(createDto)).Returns(entity);
            _mapperMock.Setup(m => m.Map<CityDto>(entity)).Returns(dto);

            var result = await _manager.CreateAsync(createDto);

            _repoMock.Verify(r => r.AddAsync(entity), Times.Once);
            _uowMock.Verify(u => u.CommitAsync(), Times.Once);
            Assert.Equal(100, result.Id);
            Assert.Same(dto, result);
        }

        [Fact]
        public async Task UpdateAsync_WhenNotFound_ReturnsFalseAndMessage()
        {
            _repoMock.Setup(r => r.GetByIdAsync(It.IsAny<int>()))
                     .ReturnsAsync((City?)null);
            _localizerMock.Setup(l => l.Get("CityNotFound"))
                          .Returns("City not found.");

            var (ok, msg) = await _manager.UpdateAsync(5, new CreateCityDto());

            Assert.False(ok);
            Assert.Equal("City not found.", msg);
            _repoMock.Verify(r => r.UpdateAsync(It.IsAny<City>()), Times.Never);
            _uowMock.Verify(u => u.CommitAsync(), Times.Never);
        }

        [Fact]
        public async Task UpdateAsync_WhenFound_UpdatesAndCommits_ReturnsTrueAndMessage()
        {
            var existing = new City { Id = 5 };
            _repoMock.Setup(r => r.GetByIdAsync(5)).ReturnsAsync(existing);
            _localizerMock.Setup(l => l.Get("CityUpdated"))
                          .Returns("City updated.");

            var dto = new CreateCityDto { NameEn = "X" };
            var (ok, msg) = await _manager.UpdateAsync(5, dto);

            Assert.True(ok);
            Assert.Equal("City updated.", msg);
            _repoMock.Verify(r => r.UpdateAsync(existing), Times.Once);
            _uowMock.Verify(u => u.CommitAsync(), Times.Once);
        }

        [Fact]
        public async Task DeleteAsync_WhenNotFound_ReturnsFalseAndMessage()
        {
            _repoMock.Setup(r => r.GetByIdAsync(It.IsAny<int>()))
                     .ReturnsAsync((City?)null);
            _localizerMock.Setup(l => l.Get("CityNotFound"))
                          .Returns("City not found.");

            var (ok, msg) = await _manager.DeleteAsync(9);

            Assert.False(ok);
            Assert.Equal("City not found.", msg);
            _repoMock.Verify(r => r.DeleteAsync(It.IsAny<City>()), Times.Never);
            _uowMock.Verify(u => u.CommitAsync(), Times.Never);
        }

        [Fact]
        public async Task DeleteAsync_WhenFound_DeletesAndCommits_ReturnsTrueAndMessage()
        {
            var existing = new City { Id = 9 };
            _repoMock.Setup(r => r.GetByIdAsync(9)).ReturnsAsync(existing);
            _localizerMock.Setup(l => l.Get("CityDeleted"))
                          .Returns("City deleted.");

            var (ok, msg) = await _manager.DeleteAsync(9);

            Assert.True(ok);
            Assert.Equal("City deleted.", msg);
            _repoMock.Verify(r => r.DeleteAsync(existing), Times.Once);
            _uowMock.Verify(u => u.CommitAsync(), Times.Once);
        }
    }
}
