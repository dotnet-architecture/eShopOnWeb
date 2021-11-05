using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace Microsoft.eShopWeb.UnitTests.ApplicationCore.Extensions;

public class TestParent : IEquatable<TestParent>
{
    public int Id { get; set; }

    public string Name { get; set; }

    public IEnumerable<TestChild> Children { get; set; }

    public bool Equals([AllowNull] TestParent other) =>
        other?.Id == Id && other?.Name == Name &&
        (other?.Children is null && Children is null ||
        (other?.Children?.Zip(Children)?.All(t => t.First?.Equals(t.Second) ?? false) ?? false));
}
