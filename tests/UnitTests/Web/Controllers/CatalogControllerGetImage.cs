using ApplicationCore.Exceptions;
using ApplicationCore.Interfaces;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.eShopWeb.Controllers;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace UnitTests
{
    public class CatalogControllerGetImage
    {
        private Mock<IImageService> _mockImageService = new Mock<IImageService>();
        private Mock<IAppLogger<CatalogController>> _mockLogger = new Mock<IAppLogger<CatalogController>>();
        private CatalogController _controller;
        private int _testImageId = 123;
        private byte[] _testBytes = { 0x01, 0x02, 0x03 };

        public CatalogControllerGetImage()
        {
            _controller = new CatalogController(null, null, _mockImageService.Object,
                _mockLogger.Object);
        }

        [Fact]
        public void CallsImageServiceWithId()
        {
            SetupImageWithTestBytes();

            _controller.GetImage(_testImageId);
            _mockImageService.Verify();
        }

        [Fact]
        public void ReturnsFileResultWithBytesGivenSuccess()
        {
            SetupImageWithTestBytes();

            var result = _controller.GetImage(_testImageId);

            var fileResult = Assert.IsType<FileContentResult>(result);
            var bytes = Assert.IsType<byte[]>(fileResult.FileContents);
        }

        [Fact]
        public void ReturnsNotFoundResultGivenImageMissingException()
        {
            SetupMissingImage();

            var result = _controller.GetImage(_testImageId);

            var actionResult = Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public void LogsWarningGivenImageMissingException()
        {
            SetupMissingImage();
            _mockLogger.Setup(l => l.LogWarning(It.IsAny<string>()))
                .Verifiable();

            _controller.GetImage(_testImageId);

            _mockLogger.Verify();
        }

        private void SetupMissingImage()
        {
            _mockImageService
                .Setup(i => i.GetImageBytesById(_testImageId))
                .Throws(new CatalogImageMissingException("missing image"));
        }

        private void SetupImageWithTestBytes()
        {
            _mockImageService
                .Setup(i => i.GetImageBytesById(_testImageId))
                .Returns(_testBytes)
                .Verifiable();
        }
    }
}
