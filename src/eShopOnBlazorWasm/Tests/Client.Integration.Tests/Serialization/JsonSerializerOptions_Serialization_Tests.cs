namespace JsonSerializerOptions
{
  using Shouldly;
  using System;
  using System.Text.Json;
  using eShopOnBlazorWasm.JsonSerializer.Tests;

  public class JsonSerializer_Should
  {
    public void SerializeAndDeserializePerson()
    {
      var jsonSerializerOptions = new JsonSerializerOptions();
      var person = new Person { FirstName = "Steve", LastName = "Cramer", BirthDay = new DateTime(1967, 09, 27) };
      string json = JsonSerializer.Serialize(person, jsonSerializerOptions);
      Person parsed = JsonSerializer.Deserialize<Person>(json, jsonSerializerOptions);
      parsed.BirthDay.ShouldBe(person.BirthDay);
      parsed.FirstName.ShouldBe(person.FirstName);
      parsed.LastName.ShouldBe(person.LastName);
    }
  }
}
