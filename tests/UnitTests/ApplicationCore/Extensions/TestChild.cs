using System;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;

namespace Microsoft.eShopWeb.UnitTests.ApplicationCore.Extensions
{
    [DebuggerDisplay("Id={Id}, Date={Date}")]
    public class TestChild : IEquatable<TestChild>
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public DateTime Date { get; set; } = DateTime.UtcNow;

        public bool Equals([AllowNull] TestChild other) =>
            other?.Date == Date && other?.Id == Id;
    }
}
