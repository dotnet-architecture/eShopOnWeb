using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Nodes;
using System.Threading.Tasks;
using BlazorAdmin.Helpers;
using BlazorAdmin.Services;
using BlazorShared.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.eShopWeb.ApplicationCore.Entities.OrderAggregate;
using Microsoft.eShopWeb.ApplicationCore.Interfaces;


namespace BlazorAdmin.Pages.OrderPage;

public partial class OrderList : BlazorComponent
{
    //inject HttpService
    [Inject]
    public HttpService HttpService { get; set; }

    private List<OrderListResponse> Orders = new List<OrderListResponse>();

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            Orders = HttpService.HttpGet<List<OrderListResponse>>("orders").Result;
            CallRequestRefresh();
        }

        await base.OnAfterRenderAsync(firstRender);
    }


}
