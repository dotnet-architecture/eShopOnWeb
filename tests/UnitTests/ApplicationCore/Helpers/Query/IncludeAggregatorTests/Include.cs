using Microsoft.eShopWeb.ApplicationCore.Helpers.Query;
using Xunit;

namespace Microsoft.eShopWeb.UnitTests.ApplicationCore.Helpers.Query.IncludeAggregatorTests
{
    public class Include
    {
        [Fact]
        public void Should_ReturnIncludeQueryWithCorrectPath_IfIncludeSimpleType()
        {
            var includeAggregator = new IncludeAggregator<Person>();

            // There may be ORM libraries where including a simple type makes sense.
            var includeQuery = includeAggregator.Include(p => p.Age);

            Assert.Contains(includeQuery.Paths, path => path == nameof(Person.Age));
        }

        [Fact]
        public void Should_ReturnIncludeQueryWithCorrectPath_IfIncludeFunction()
        {
            var includeAggregator = new IncludeAggregator<Person>();

            // This include does not make much sense, but it should at least do not modify the paths.
            var includeQuery = includeAggregator.Include(p => p.FavouriteBook.GetNumberOfSales());

            Assert.Contains(includeQuery.Paths, path => path == nameof(Person.FavouriteBook));
        }

        [Fact]
        public void Should_ReturnIncludeQueryWithCorrectPath_IfIncludeObject()
        {
            var includeAggregator = new IncludeAggregator<Person>();
            var includeQuery = includeAggregator.Include(p => p.FavouriteBook.Author);

            Assert.Contains(includeQuery.Paths, path => path == $"{nameof(Person.FavouriteBook)}.{nameof(Book.Author)}");
        }

        [Fact]
        public void Should_ReturnIncludeQueryWithCorrectPath_IfIncludeCollection()
        {
            var includeAggregator = new IncludeAggregator<Book>();
            var includeQuery = includeAggregator.Include(o => o.Author.Friends);

            Assert.Contains(includeQuery.Paths, path => path == $"{nameof(Book.Author)}.{nameof(Person.Friends)}");
        }
    }
}
