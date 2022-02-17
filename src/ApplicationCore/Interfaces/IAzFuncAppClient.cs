using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Microsoft.eShopWeb.ApplicationCore.Interfaces;

public interface IAzFuncAppClient
{
    Task PostAsync(OrderDetailsProcessingDto model, CancellationToken cancellation = default);
}

public record OrderDetailsProcessingDto(
    decimal FinalPrice,
    ShippingAddressProcessingDto ShippingAddress,
    IEnumerable<OrderItemProcessingDto> OrderItems);

public record ShippingAddressProcessingDto(
    string Street,
    string City,
    string State,
    string Country,
    string ZipCode);

public record OrderItemProcessingDto(
    string ProductName,
    decimal UnitPrice);
