using Microsoft.eShopWeb.ApplicationCore.Entities;
using Microsoft.eShopWeb.ApplicationCore.Interfaces;
using Microsoft.eShopWeb.Web.Services;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Internal;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace Microsoft.eShopWeb.UnitTests.ApplicationCore.Services.CatalogViewModelServiceTests
{
    public class GetBrands
    {
        private readonly Mock<ILogger<CatalogViewModelService>> _logger;
        private readonly Mock<ILoggerFactory> _loggerFactory;
        private readonly Mock<IAsyncRepository<CatalogItem>> _itemRepository;
        private readonly Mock<IAsyncRepository<CatalogBrand>> _brandRepository;
        private readonly Mock<IAsyncRepository<CatalogType>> _typeRepository;
        private readonly Mock<IUriComposer> _uriComposer;

        public GetBrands()
        {
            _logger = new Mock<ILogger<CatalogViewModelService>>();
            _loggerFactory = new Mock<ILoggerFactory>();

            _logger.Setup(l => l.Log(LogLevel.Information, 0, It.IsAny<FormattedLogValues>(), It.IsAny<Exception>(),
        It.IsAny<Func<object, Exception, string>>()));

            _itemRepository = new Mock<IAsyncRepository<CatalogItem>>();
            _brandRepository = new Mock<IAsyncRepository<CatalogBrand>>();
            _typeRepository = new Mock<IAsyncRepository<CatalogType>>();
            _uriComposer = new Mock<IUriComposer>();

            var catalogViewModelService = new CatalogViewModelService(_loggerFactory.Object,
                _itemRepository.Object,
                _brandRepository.Object,
                _typeRepository.Object,
                _uriComposer.Object);
        }

        [Fact]
        public async Task CallsBrandRepositoryOnce()
        {
            IReadOnlyList <CatalogBrand> catalogBrands = new List<CatalogBrand>
            {
                new CatalogBrand { Brand = "1", Id = 1 }
            };

            _brandRepository.Setup(x => x.ListAllAsync()).ReturnsAsync(catalogBrands);
            
            var catalogViewModelService = new CatalogViewModelService(_loggerFactory.Object,
                                                                    _itemRepository.Object,
                                                                    _brandRepository.Object,
                                                                    _typeRepository.Object,
                                                                    _uriComposer.Object);

            var result = catalogViewModelService.GetBrands();

            _brandRepository.Verify(x => x.ListAllAsync(), Times.Once);
        }
    }
}
