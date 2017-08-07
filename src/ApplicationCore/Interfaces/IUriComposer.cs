using Microsoft.eShopWeb.ApplicationCore.Entities;
using System.Collections.Generic;

namespace ApplicationCore.Interfaces
{

    public interface IUriComposer
    {
        string ComposePicUri(string uriTemplate);
    }
}
