using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlazorShared.Interfaces;
using BlazorShared.Models;
using Microsoft.Extensions.Logging;


namespace BlazorAdmin.Services;

public class OrderService : IOrderService
{
    private readonly HttpService _httpService;
    private readonly ILogger<OrderService> _logger;

    public OrderService(
        HttpService httpService,
        ILogger<OrderService> logger)
    {
        _httpService = httpService;
        _logger = logger;
    }

    public async Task<string> EditAsync(OrderModel order)
    {
        return (await _httpService.HttpPut<EditOrderResponse>("orders", order)).Message;
    }

    public async Task<List<OrderModel>> ListAsync()
    {
        _logger.LogInformation("Fetching orders items from API.");

        var orders = await _httpService.HttpGet<PagedOrderResponse>($"orders");

        var items = orders.orders;

        return items;
    }
}
