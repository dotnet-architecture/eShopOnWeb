using System.Diagnostics.CodeAnalysis;

namespace Microsoft.eShopWeb.UnitTests.ApplicationCore.Extensions;

public class TestParent : IEquatable<TestParent>
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public IEnumerable<TestChild>? Children { get; set; }

    public bool Equals([AllowNull] TestParent other) 
    {
        if (other?.Id == Id && other?.Name == Name)
        {
            if (Children is null)
            {
                return other?.Children is null;
            }

            return other?.Children?.Zip(Children).All(t => t.First?.Equals(t.Second) ?? false) ?? false;
        }

        return false;
    }
}
