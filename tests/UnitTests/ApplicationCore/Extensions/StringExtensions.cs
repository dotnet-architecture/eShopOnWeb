using Xunit;

namespace Microsoft.eShopWeb.UnitTests.ApplicationCore.Extensions
{
    public class StringExtensions
    {
        [Fact]
        public void CorrectlySerializesAndDeserializesObject()
        {
            var foo = new Foo
            {
                Id = 7,
                Name = "Test name",
                Children = new[]
                {
                    new Bar(),
                    new Bar(),
                    new Bar()
                }
            };

            var json = foo.ToJson();
            var result = json.FromJson<Foo>();
            Assert.Equal(foo, result);
        }

        [
            Theory,
            InlineData("{ \"id\": 9, \"name\": \"Another test\" }", 9, "Another test"),
            InlineData("{ \"id\": 3124, \"name\": \"Ugh, this doesn't matter...\" }", 3124, "Ugh, this doesn't matter..."),
        ]
        public void CorrectlyDeserializesJson(string json, int expectedId, string expectedName) =>
            Assert.Equal(new Foo { Id = expectedId, Name = expectedName }, json.FromJson<Foo>());

    }
}