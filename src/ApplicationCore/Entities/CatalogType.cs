using Microsoft.eShopWeb.ApplicationCore.Interfaces;

namespace Microsoft.eShopWeb.ApplicationCore.Entities;

public class CatalogType : BaseEntity, IAggregateRoot
{
    public string Type { get; private set; }
    public CatalogType(string type)
    {
        Type = type;
    }
}
