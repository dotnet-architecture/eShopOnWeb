using ApplicationCore.Interfaces;
using System.Threading.Tasks;
using Microsoft.eShopWeb.ApplicationCore.Entities;
using System;
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

        public async Task<BasketViewModel> GetOrCreateBasketForUser(string userName)
        {
            var basketSpec = new BasketWithItemsSpecification(userName);
            var basket = _basketRepository.List(basketSpec).FirstOrDefault();

            if(basket == null)
            {
                return await CreateBasketForUser(userName);
            }
            return CreateViewModelFromBasket(basket);
        }

        private BasketViewModel CreateViewModelFromBasket(Basket basket)
        {
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

        public async Task Checkout(int basketId)
        {
            var basket = _basketRepository.GetById(basketId);
            
            // TODO: Actually Process the order

            _basketRepository.Delete(basket);
        }

        public Task TransferBasket(string anonymousId, string userName)
        {
            var basketSpec = new BasketWithItemsSpecification(anonymousId);
            var basket = _basketRepository.List(basketSpec).FirstOrDefault();
            if (basket == null) return Task.CompletedTask;
            basket.BuyerId = userName;
            _basketRepository.Update(basket);
            return Task.CompletedTask;
        }
    }
}
