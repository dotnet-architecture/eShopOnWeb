using Microsoft.eShopWeb.Web.Services;
using Microsoft.Extensions.Logging;
using System;

namespace Microsoft.eShopWeb.UnitTests.ApplicationCore.Services.CatalogViewModelServiceTests
{
    public class FakeLoggerFactory : ILoggerFactory
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
    }
}
