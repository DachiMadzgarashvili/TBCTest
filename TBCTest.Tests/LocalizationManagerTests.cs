using Moq;
using Xunit;
using TBCTest.Data;
using TBCTest.Managers;
using TBCTest.Models;
using TBCTest.Repositories;
using TBCTest.Services;
using Assert = Xunit.Assert;

namespace TBCTest.Tests
{
    public class LocalizationManagerTests
    {
        private readonly Mock<ILocalizationRepository> _repoMock;
        private readonly Mock<IUnitOfWork> _uowMock;
        private readonly Mock<IDbLocalizationService> _localizerMock;
        private readonly LocalizationManager _manager;

        public LocalizationManagerTests()
        {
            _repoMock = new Mock<ILocalizationRepository>();
            _uowMock = new Mock<IUnitOfWork>();
            _localizerMock = new Mock<IDbLocalizationService>();

            _manager = new LocalizationManager(
                _repoMock.Object,
                _uowMock.Object,
                _localizerMock.Object
            );
        }

        [Fact]
        public async Task GetAllAsync_ReturnsAllEntries()
        {
            var list = new List<Localization>
            {
                new Localization { Id = 1, Key="A", Language="en-US", Value="a" },
                new Localization { Id = 2, Key="B", Language="ka-GE", Value="ბ" }
            };
            _repoMock.Setup(r => r.GetAllAsync()).ReturnsAsync(list);

            var result = await _manager.GetAllAsync();

            Assert.Equal(list, result);
        }

        [Fact]
        public async Task GetByKeyAsync_ReturnsMatchingEntries()
        {
            var list = new List<Localization>
            {
                new Localization { Id = 3, Key="Test", Language="en-US", Value="test" }
            };
            _repoMock.Setup(r => r.GetByKeyAsync("Test")).ReturnsAsync(list);

            var result = await _manager.GetByKeyAsync("Test");

            Assert.Equal(list, result);
        }

        [Fact]
        public async Task SearchAsync_DelegatesToRepository()
        {
            var list = new List<Localization>();
            _repoMock.Setup(r => r.SearchAsync("k", "l")).ReturnsAsync(list);

            var result = await _manager.SearchAsync("k", "l");

            Assert.Same(list, result);
        }

        [Fact]
        public async Task UpdateAsync_WhenNotFound_ReturnsFalseAndMessage()
        {
            _repoMock.Setup(r => r.GetByIdAsync(It.IsAny<int>())).ReturnsAsync((Localization?)null);
            _localizerMock.Setup(l => l.Get("LocalizationNotFound"))
                          .Returns("Not found.");

            var (ok, msg) = await _manager.UpdateAsync(11, new Localization());

            Assert.False(ok);
            Assert.Equal("Not found.", msg);
            _repoMock.Verify(r => r.UpdateAsync(It.IsAny<Localization>()), Times.Never);
            _uowMock.Verify(u => u.CommitAsync(), Times.Never);
        }

        [Fact]
        public async Task UpdateAsync_WhenFound_UpdatesAndCommits_ReturnsTrueAndMessage()
        {
            var existing = new Localization { Id = 11 };
            _repoMock.Setup(r => r.GetByIdAsync(11)).ReturnsAsync(existing);
            _localizerMock.Setup(l => l.Get("LocalizationUpdated"))
                          .Returns("Updated.");

            var updated = new Localization { Key = "K", Language = "en-US", Value = "V" };
            var (ok, msg) = await _manager.UpdateAsync(11, updated);

            Assert.True(ok);
            Assert.Equal("Updated.", msg);
            _repoMock.Verify(r => r.UpdateAsync(existing), Times.Once);
            _uowMock.Verify(u => u.CommitAsync(), Times.Once);
        }
    }
}
