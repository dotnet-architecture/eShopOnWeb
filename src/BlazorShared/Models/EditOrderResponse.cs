using BlazorShared.Enums;

namespace BlazorShared.Models;

public class EditOrderResult
{
    public int Id { get; set; }
    public OrderStatus OrderStatus { get; set; }
}
