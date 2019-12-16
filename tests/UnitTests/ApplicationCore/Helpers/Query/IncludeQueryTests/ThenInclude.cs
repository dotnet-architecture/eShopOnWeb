using Microsoft.eShopWeb.ApplicationCore.Helpers.Query;
using Microsoft.eShopWeb.UnitTests.Builders;
using System.Linq;
using Xunit;

namespace Microsoft.eShopWeb.UnitTests.ApplicationCore.Helpers.Query.IncludeQueryTests
{
    public class ThenInclude
    {
        private IncludeQueryBuilder _includeQueryBuilder = new IncludeQueryBuilder();

        [Fact]
        public void Should_ReturnIncludeQueryWithCorrectPath_IfIncludeSimpleType()
        {
            var includeQuery = _includeQueryBuilder.WithObjectAsPreviousProperty();
            var pathBeforeInclude = includeQuery.Paths.First();

            // There may be ORM libraries where including a simple type makes sense.
            var newIncludeQuery = includeQuery.ThenInclude(p => p.Age);
            var pathAfterInclude = newIncludeQuery.Paths.First();
            var expectedPath = $"{pathBeforeInclude}.{nameof(Person.Age)}";

            Assert.Equal(expectedPath, pathAfterInclude);
        }

        [Fact]
        public void Should_ReturnIncludeQueryWithCorrectPath_IfIncludeFunction()
        {
            var includeQuery = _includeQueryBuilder.WithObjectAsPreviousProperty();
            var pathBeforeInclude = includeQuery.Paths.First();

            // This include does not make much sense, but it should at least not modify the paths.
            var newIncludeQuery = includeQuery.ThenInclude(p => p.GetQuote());
            var pathAfterInclude = newIncludeQuery.Paths.First();

            Assert.Equal(pathBeforeInclude, pathAfterInclude);
        }

        [Fact]
        public void Should_ReturnIncludeQueryWithCorrectPath_IfIncludeObject()
        {
            var includeQuery = _includeQueryBuilder.WithObjectAsPreviousProperty();
            var pathBeforeInclude = includeQuery.Paths.First();

            var newIncludeQuery = includeQuery.ThenInclude(p => p.FavouriteBook);
            var pathAfterInclude = newIncludeQuery.Paths.First();
            var expectedPath = $"{pathBeforeInclude}.{nameof(Person.FavouriteBook)}";

            Assert.Equal(expectedPath, pathAfterInclude);
        }

        [Fact]
        public void Should_ReturnIncludeQueryWithCorrectPath_IfIncludeCollection()
        {
            var includeQuery = _includeQueryBuilder.WithObjectAsPreviousProperty();
            var pathBeforeInclude = includeQuery.Paths.First();

            var newIncludeQuery = includeQuery.ThenInclude(p => p.Friends);
            var pathAfterInclude = newIncludeQuery.Paths.First();
            var expectedPath = $"{pathBeforeInclude}.{nameof(Person.Friends)}";

            Assert.Equal(expectedPath, pathAfterInclude);
        }

        [Fact]
        public void Should_ReturnIncludeQueryWithCorrectPath_IfIncludePropertyOverCollection()
        {
            var includeQuery = _includeQueryBuilder.WithCollectionAsPreviousProperty();
            var pathBeforeInclude = includeQuery.Paths.First();

            var newIncludeQuery = includeQuery.ThenInclude(p => p.FavouriteBook);
            var pathAfterInclude = newIncludeQuery.Paths.First();
            var expectedPath = $"{pathBeforeInclude}.{nameof(Person.FavouriteBook)}";

            Assert.Equal(expectedPath, pathAfterInclude);
        }
    }
}
