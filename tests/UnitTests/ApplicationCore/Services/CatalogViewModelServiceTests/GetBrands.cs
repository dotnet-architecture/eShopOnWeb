using Microsoft.eShopWeb.ApplicationCore.Entities;
using Microsoft.eShopWeb.ApplicationCore.Interfaces;
using Microsoft.eShopWeb.Web.Services;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace Microsoft.eShopWeb.UnitTests.ApplicationCore.Services.CatalogViewModelServiceTests
{
    public class GetBrands
    {
        private readonly FakeLoggerFactory _fakeLoggerFactory;
        private readonly Mock<IAsyncRepository<CatalogItem>> _itemRepository;
        private readonly Mock<IAsyncRepository<CatalogBrand>> _brandRepository;
        private readonly Mock<IAsyncRepository<CatalogType>> _typeRepository;
        private readonly Mock<IUriComposer> _uriComposer;

        public GetBrands()
        {
            _fakeLoggerFactory = new FakeLoggerFactory();
            _itemRepository = new Mock<IAsyncRepository<CatalogItem>>();
            _brandRepository = new Mock<IAsyncRepository<CatalogBrand>>();
            _typeRepository = new Mock<IAsyncRepository<CatalogType>>();
            _uriComposer = new Mock<IUriComposer>();
        }

        [Fact]
        public async Task CallsBrandRepositoryOnce()
        {
            IReadOnlyList <CatalogBrand> catalogBrands = new List<CatalogBrand>
            {
                new CatalogBrand { Brand = "1", Id = 1 }
            };

            _brandRepository.Setup(x => x.ListAllAsync()).ReturnsAsync(catalogBrands);

            var catalogViewModelService = new CatalogViewModelService(_fakeLoggerFactory,
                                                                    _itemRepository.Object,
                                                                    _brandRepository.Object,
                                                                    _typeRepository.Object,
                                                                    _uriComposer.Object);

            var result = await catalogViewModelService.GetBrands();
            
            _brandRepository.Verify(x => x.ListAllAsync(), Times.Once);
        }

        //https://ardalis.com/testing-logging-in-aspnet-core
        private class FakeLogger : ILogger<CatalogViewModelService>
        {
            public IDisposable BeginScope<TState>(TState state)
            {
                return null;
            }

            public bool IsEnabled(LogLevel logLevel)
            {
                return true;
            }

            public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
            {

            }
        }

        private class FakeLoggerFactory : ILoggerFactory
        {
            public void AddProvider(ILoggerProvider provider)
            {
            }

            public ILogger CreateLogger(string categoryName)
            {
                return new FakeLogger();
            }

            public void Dispose()
            {

            }
        }
    }
}
