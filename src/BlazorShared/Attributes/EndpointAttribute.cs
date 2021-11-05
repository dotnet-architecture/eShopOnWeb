using System;

namespace BlazorShared.Attributes;

public class EndpointAttribute : Attribute
{
    public string Name { get; set; }
}
