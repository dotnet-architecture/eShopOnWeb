using Microsoft.eShopWeb.ApplicationCore.Helpers.Query;
using Microsoft.eShopWeb.ApplicationCore.Interfaces;
using Microsoft.eShopWeb.UnitTests.ApplicationCore.Helpers.Query;
using System;
using System.Collections.Generic;

namespace Microsoft.eShopWeb.UnitTests.Builders
{
    public class IncludeQueryBuilder
    {
        public IncludeQuery<Person, List<Person>> WithCollectionAsPreviousProperty()
        {
            var pathMap = new Dictionary<IIncludeQuery, string>();
            var query = new IncludeQuery<Person, List<Person>>(pathMap);
            pathMap[query] = nameof(Person.Friends);

            return query;
        }

        public IncludeQuery<Book, Person> WithObjectAsPreviousProperty()
        {
            var pathMap = new Dictionary<IIncludeQuery, string>();
            var query = new IncludeQuery<Book, Person>(pathMap);
            pathMap[query] = nameof(Book.Author);

            return query;
        }
    }
}
