namespace Microsoft.eShopWeb.PublicApi;

public class AResponse<T> 
{
    public string Message { get; set; }
    public bool Successful { get; set; }

    public T Data { get; set; }

}
