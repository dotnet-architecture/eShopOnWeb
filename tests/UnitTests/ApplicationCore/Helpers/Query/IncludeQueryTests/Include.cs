using Microsoft.eShopWeb.ApplicationCore.Helpers.Query;
using Microsoft.eShopWeb.UnitTests.Builders;
using Xunit;

namespace Microsoft.eShopWeb.UnitTests.ApplicationCore.Helpers.Query.IncludeQueryTests
{
    public class Include
    {
        private IncludeQueryBuilder _includeQueryBuilder = new IncludeQueryBuilder();

        [Fact]
        public void Should_ReturnIncludeQueryWithCorrectPath_IfIncludeSimpleType()
        {
            var includeQuery = _includeQueryBuilder.WithObjectAsPreviousProperty();

            // There may be ORM libraries where including a simple type makes sense.
            var newIncludeQuery = includeQuery.Include(b => b.Title);

            Assert.Contains(newIncludeQuery.Paths, path => path == nameof(Book.Title));
        }

        [Fact]
        public void Should_ReturnIncludeQueryWithCorrectPath_IfIncludeFunction()
        {
            var includeQuery = _includeQueryBuilder.WithObjectAsPreviousProperty();

            // This include does not make much sense, but it should at least do not modify paths.
            var newIncludeQuery = includeQuery.Include(b => b.GetNumberOfSales());

            // The resulting paths should not include number of sales.
            Assert.DoesNotContain(newIncludeQuery.Paths, path => path == nameof(Book.GetNumberOfSales));
        }

        [Fact]
        public void Should_ReturnIncludeQueryWithCorrectPath_IfIncludeObject()
        {
            var includeQuery = _includeQueryBuilder.WithObjectAsPreviousProperty();
            var newIncludeQuery = includeQuery.Include(b => b.Author);

            Assert.Contains(newIncludeQuery.Paths, path => path == nameof(Book.Author));
        }

        [Fact]
        public void Should_ReturnIncludeQueryWithCorrectPath_IfIncludeCollection()
        {
            var includeQuery = _includeQueryBuilder.WithObjectAsPreviousProperty();

            var newIncludeQuery = includeQuery.Include(b => b.Author.Friends);
            var expectedPath = $"{nameof(Book.Author)}.{nameof(Person.Friends)}";

            Assert.Contains(newIncludeQuery.Paths, path => path == expectedPath);
        }

        [Fact]
        public void Should_IncreaseNumberOfPathsByOne()
        {
            var includeQuery = _includeQueryBuilder.WithObjectAsPreviousProperty();
            var numberOfPathsBeforeInclude = includeQuery.Paths.Count;

            var newIncludeQuery = includeQuery.Include(b => b.Author.Friends);
            var numberOfPathsAferInclude = newIncludeQuery.Paths.Count;

            var expectedNumerOfPaths = numberOfPathsBeforeInclude + 1;

            Assert.Equal(expectedNumerOfPaths, numberOfPathsAferInclude);
        }

        [Fact]
        public void Should_NotModifyAnotherPath()
        {
            var includeQuery = _includeQueryBuilder.WithObjectAsPreviousProperty();
            var pathsBeforeInclude = includeQuery.Paths;

            var newIncludeQuery = includeQuery.Include(b => b.Author.Friends);
            var pathsAfterInclude = newIncludeQuery.Paths;

            Assert.Subset(pathsAfterInclude, pathsBeforeInclude);
        }
    }
}
