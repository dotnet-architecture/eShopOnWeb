using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace Microsoft.eShopWeb.UnitTests.ApplicationCore.Extensions
{
    public class Foo : IEquatable<Foo>
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public IEnumerable<Bar> Children { get; set; }

        public bool Equals([AllowNull] Foo other) =>
            other?.Id == Id && other?.Name == Name &&
            (other?.Children is null && Children is null ||
            (other?.Children?.Zip(Children)?.All(t => t.First?.Equals(t.Second) ?? false) ?? false));
    }

    [DebuggerDisplay("Id={Id}, Date={Date}")]
    public class Bar : IEquatable<Bar>
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public DateTime Date { get; set; } = DateTime.UtcNow;

        public bool Equals([AllowNull] Bar other) =>
            other?.Date == Date && other?.Id == Id;


    }
}