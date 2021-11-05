using Xunit;

namespace Microsoft.eShopWeb.UnitTests.ApplicationCore.Extensions;

public class JsonExtensions
{
    [Fact]
    public void CorrectlySerializesAndDeserializesObject()
    {
        var testParent = new TestParent
        {
            Id = 7,
            Name = "Test name",
            Children = new[]
            {
                    new TestChild(),
                    new TestChild(),
                    new TestChild()
                }
        };

        var json = testParent.ToJson();
        var result = json.FromJson<TestParent>();
        Assert.Equal(testParent, result);
    }

    [
        Theory,
        InlineData("{ \"id\": 9, \"name\": \"Another test\" }", 9, "Another test"),
        InlineData("{ \"id\": 3124, \"name\": \"Test Value 1\" }", 3124, "Test Value 1"),
    ]
    public void CorrectlyDeserializesJson(string json, int expectedId, string expectedName) =>
        Assert.Equal(new TestParent { Id = expectedId, Name = expectedName }, json.FromJson<TestParent>());

}
