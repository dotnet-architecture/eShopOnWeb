using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlazorAdmin.Helpers;
using BlazorAdmin.Services;
using Microsoft.AspNetCore.Components;
using BlazorShared.Models;
using Microsoft.eShopWeb.ApplicationCore.Entities;
using BlazorShared.Enums;



namespace BlazorAdmin.Pages.OrderPage;

public partial class OrderList : BlazorComponent
{
    [Inject]
    public HttpService HttpService { get; set; }

    private List<Order> Orders = new List<Order>();
    private List<OrderItem> orderItems = new List<OrderItem>();
    private List<string> orderStatus = new List<string>();
    private List<int> orderIds = new List<int>();

    private OrderDetails OrderDetailsComponent { get; set; }

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            Orders = await HttpService.HttpGet<List<Order>>("orders");
            foreach (var order in Orders)
            {
                orderItems.AddRange(order.OrderItems);
            }
            CallRequestRefresh();
        }

        await base.OnAfterRenderAsync(firstRender);
    }

    private async void DetailsClick(int id, OrderStatus status)
    {
        await OrderDetailsComponent.Open(id, status);
    }

    private async Task ReloadOrders()
    {
        Orders = await HttpService.HttpGet<List<Order>>("orders");
        StateHasChanged();
    }
}
