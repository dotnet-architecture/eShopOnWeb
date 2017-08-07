using ApplicationCore.Interfaces;
using System.Threading.Tasks;
using Microsoft.eShopWeb.ApplicationCore.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using Infrastructure.Data;
using System.Linq;
using Microsoft.eShopWeb.ViewModels;
using System.Collections.Generic;
using ApplicationCore.Specifications;

namespace Web.Services
{
    public class BasketService : IBasketService
    {
        private readonly IRepository<Basket> _basketRepository;
        private readonly IUriComposer _uriComposer;
        private readonly IRepository<CatalogItem> _itemRepository;

        public BasketService(IRepository<Basket> basketRepository,
            IRepository<CatalogItem> itemRepository,
            IUriComposer uriComposer)
        {
            _basketRepository = basketRepository;
            _uriComposer = uriComposer;
            _itemRepository = itemRepository;
        }
        public async Task<BasketViewModel> GetBasket(int basketId)
        {
            var basketSpec = new BasketWithItemsSpecification(basketId);
            var basket = _basketRepository.List(basketSpec).FirstOrDefault();
            if (basket == null)
            {
                return await CreateBasket();
            }

            var viewModel = new BasketViewModel();
            viewModel.Id = basket.Id;
            viewModel.BuyerId = basket.BuyerId;
            viewModel.Items = basket.Items.Select(i =>
                            {
                                var itemModel = new BasketItemViewModel()
                                {
                                    Id = i.Id,
                                    UnitPrice = i.UnitPrice,
                                    Quantity = i.Quantity,
                                    CatalogItemId = i.CatalogItemId

                                };
                                var item = _itemRepository.GetById(i.CatalogItemId);
                                itemModel.PictureUrl = _uriComposer.ComposePicUri(item.PictureUri);
                                itemModel.ProductName = item.Name;
                                return itemModel;
                            })
                            .ToList();
            return viewModel;
        }

        public Task<BasketViewModel> CreateBasket()
        {
            return CreateBasketForUser(null);
        }

        public async Task<BasketViewModel> CreateBasketForUser(string userId)
        {
            var basket = new Basket() { BuyerId = userId };
            _basketRepository.Add(basket);

            return new BasketViewModel()
            {
                BuyerId = basket.BuyerId,
                Id = basket.Id,
                Items = new List<BasketItemViewModel>()
            };
        }

        public async Task AddItemToBasket(int basketId, int catalogItemId, decimal price, int quantity)
        {
            var basket = _basketRepository.GetById(basketId);

            basket.AddItem(catalogItemId, price, quantity);

            _basketRepository.Update(basket);
        }
    }
}
