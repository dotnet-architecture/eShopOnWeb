using ApplicationCore.Exceptions;
using ApplicationCore.Interfaces;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.eShopWeb.Controllers;
using Moq;
using Xunit;

namespace UnitTests
{
    public class CatalogControllerGetImage
    {
        private Mock<IImageService> _mockImageService = new Mock<IImageService>();
        private CatalogController _controller;
        private int _testImageId = 123;
        private byte[] _testBytes = { 0x01, 0x02, 0x03 };

        public CatalogControllerGetImage()
        {
            _controller = new CatalogController(null, null, _mockImageService.Object);
        }

        [Fact]
        public void CallsImageServiceWithId()
        {
            _mockImageService
                .Setup(i => i.GetImageBytesById(_testImageId))
                .Returns(_testBytes)
                .Verifiable();

            _controller.GetImage(_testImageId);
            _mockImageService.Verify();
        }

        [Fact]
        public void ReturnsFileResultWithBytesGivenSuccess()
        {
            _mockImageService
                .Setup(i => i.GetImageBytesById(_testImageId))
                .Returns(_testBytes);

            var result = _controller.GetImage(_testImageId);

            var fileResult = Assert.IsType<FileContentResult>(result);
            var bytes = Assert.IsType<byte[]>(fileResult.FileContents);
        }

        [Fact]
        public void ReturnsNotFoundResultGivenImageMissingException()
        {
            _mockImageService
                .Setup(i => i.GetImageBytesById(_testImageId))
                .Throws(new CatalogImageMissingException("missing image"));

            var result = _controller.GetImage(_testImageId);

            var actionResult = Assert.IsType<NotFoundResult>(result);
        }
    }
}
