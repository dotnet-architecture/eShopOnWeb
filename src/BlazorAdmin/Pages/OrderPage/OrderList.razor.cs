using System.Collections.Generic;
using System.Threading.Tasks;
using BlazorAdmin.Helpers;
using BlazorAdmin.Pages.CatalogItemPage;
using BlazorShared.Interfaces;
using Microsoft.eShopWeb.ApplicationCore.Entities.OrderAggregate;
using Microsoft.eShopWeb.ApplicationCore.Interfaces;

namespace BlazorAdmin.Pages.OrderPage;

public partial class OrderList : BlazorComponent
{
    [Microsoft.AspNetCore.Components.Inject]
    public IOrderService OrderService { get; set; }

    private List<Order> Orders = new List<Order>();
    //private List<OrderItem> OrderItems= new List<OrderItem>();

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            Orders = await OrderService.GetOrdersAsync();
            CallRequestRefresh();
        }

        await base.OnAfterRenderAsync(firstRender);
    }

    private Details DetailsComponent { get; set; }

    public async Task ReloadOrder()
    {
        Orders = await OrderService.GetOrdersAsync();
        StateHasChanged();
    }

    //private async void DetailsClick(int id)
    //{
    //    await DetailsComponent.Open(id);
    //}
}
