using Microsoft.eShopWeb.ApplicationCore.Entities;
using Microsoft.eShopWeb.ApplicationCore.Interfaces;
using Microsoft.eShopWeb.Web.Services;
using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace Microsoft.eShopWeb.UnitTests.ApplicationCore.Services.CatalogViewModelServiceTests
{
    public class GetTypes
    {
        private readonly FakeLoggerFactory _fakeLoggerFactory;
        private readonly Mock<IAsyncRepository<CatalogItem>> _itemRepository;
        private readonly Mock<IAsyncRepository<CatalogBrand>> _brandRepository;
        private readonly Mock<IAsyncRepository<CatalogType>> _typeRepository;
        private readonly Mock<IUriComposer> _uriComposer;

        public GetTypes()
        {
            _fakeLoggerFactory = new FakeLoggerFactory();
            _itemRepository = new Mock<IAsyncRepository<CatalogItem>>();
            _brandRepository = new Mock<IAsyncRepository<CatalogBrand>>();
            _typeRepository = new Mock<IAsyncRepository<CatalogType>>();
            _uriComposer = new Mock<IUriComposer>();
        }

        [Fact]
        public async Task CallsTypesRepositoryOnce()
        {
            IReadOnlyList<CatalogType> catalogTypes = new List<CatalogType>
            {
                new CatalogType { Type = "1", Id = 1 }
            };

            _typeRepository.Setup(x => x.ListAllAsync()).ReturnsAsync(catalogTypes);

            var catalogViewModelService = new CatalogViewModelService(_fakeLoggerFactory,
                                                                    _itemRepository.Object,
                                                                    _brandRepository.Object,
                                                                    _typeRepository.Object,
                                                                    _uriComposer.Object);

            var result = await catalogViewModelService.GetTypes();

            _typeRepository.Verify(x => x.ListAllAsync(), Times.Once);
        }
    }
}
