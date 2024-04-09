namespace Microsoft.eShopWeb.PublicApi.OrderListEndpoints;

public class ListOrderRequest : BaseRequest
{
    public int PageSize { get; init; }
    public int PageIndex { get; init; }

    public ListOrderRequest(int? pageSize, int? pageIndex)
    {
        PageSize = pageSize ?? 0;
        PageIndex = pageIndex ?? 0;
    }
}
