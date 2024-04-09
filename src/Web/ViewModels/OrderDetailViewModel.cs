namespace Microsoft.eShopWeb.Web.ViewModels;

public class OrderDetailViewModel : OrderViewModel
{
    public int UserId { get; set; }
    public List<OrderItemViewModel> OrderItems { get; set; } = new();
    public bool Shipped { get; set; }
    public int OrderId { get; set; }
}
