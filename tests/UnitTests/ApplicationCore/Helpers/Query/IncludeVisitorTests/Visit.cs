using Microsoft.eShopWeb.ApplicationCore.Helpers.Query;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Xunit;

namespace Microsoft.eShopWeb.UnitTests.ApplicationCore.Helpers.Query.IncludeVisitorTests
{
    public class Visit
    {
        [Fact]
        public void Should_SetPath_IfPassedExpressionWithSimpleType()
        {
            var visitor = new IncludeVisitor();
            Expression<Func<Book, string>> expression = (book) => book.Author.FirstName;
            visitor.Visit(expression);

            var expectedPath = $"{nameof(Book.Author)}.{nameof(Person.FirstName)}";
            Assert.Equal(expectedPath, visitor.Path);
        }

        [Fact]
        public void Should_SetPath_IfPassedExpressionWithObject()
        {
            var visitor = new IncludeVisitor();
            Expression<Func<Book, Book>> expression = (book) => book.Author.FavouriteBook;
            visitor.Visit(expression);

            var expectedPath = $"{nameof(Book.Author)}.{nameof(Person.FavouriteBook)}";
            Assert.Equal(expectedPath, visitor.Path);
        }

        [Fact]
        public void Should_SetPath_IfPassedExpressionWithCollection()
        {
            var visitor = new IncludeVisitor();
            Expression<Func<Book, List<Person>>> expression = (book) => book.Author.Friends;
            visitor.Visit(expression);

            var expectedPath = $"{nameof(Book.Author)}.{nameof(Person.Friends)}";
            Assert.Equal(expectedPath, visitor.Path);
        }

        [Fact]
        public void Should_SetPath_IfPassedExpressionWithFunction()
        {
            var visitor = new IncludeVisitor();
            Expression<Func<Book, string>> expression = (book) => book.Author.GetQuote();
            visitor.Visit(expression);

            var expectedPath = nameof(Book.Author);
            Assert.Equal(expectedPath, visitor.Path);
        }
    }
}
