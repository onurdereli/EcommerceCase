using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using AutoMapper;
using Shared.Dtos;
using Shared.Extensions;
using Shared.Messages.Events.Concrete;
using Web.Client.Abstract;
using Web.Models.Dtos;
using Web.Models.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace Web.Client.Concrete
{
    public class BasketClient: IBasketClient
    {
        private readonly IMapper _mapper;
        private readonly HttpClient _httpClient;
        private readonly ICampaignClient _campaignClient;

        public BasketClient(HttpClient httpClient, ICampaignClient campaignClient, IMapper mapper)
        {
            _httpClient = httpClient;
            _campaignClient = campaignClient;
            _mapper = mapper;
        }

        public async Task<Response<NoContent>> SaveOrUpdate(BasketViewModel basketViewModel)
        {
            var basketDto = _mapper.Map<BasketDto>(basketViewModel);

            var response = await _httpClient.PostAsJsonAsync("Baskets/SaveOrUpdateBasket", basketDto);

            return response.IsSuccessStatusCode ? Response<NoContent>.Success((int)response.StatusCode) : Response<NoContent>.Fail("error", (int)response.StatusCode);
        }
        
        public async Task<BasketViewModel> Get(string userId)
        {
            var response = await _httpClient.GetAsync($"Baskets/{userId}");

            if (!response.IsSuccessStatusCode)
            {
                return null;
            }

            var basketResponse = await response.ReadFromJsonAsync<Response<BasketViewModel>>();

            return basketResponse.Data;
        }

        public async Task<Response<NoContent>> Delete(string userId)
        {
            var response = await _httpClient.DeleteAsync($"Baskets/{userId}");

            return response.IsSuccessStatusCode ? Response<NoContent>.Success((int)response.StatusCode) : Response<NoContent>.Fail("error", (int)response.StatusCode);
        }

        public async Task AddBasketItem(string userId, BasketItemViewModel basketItemViewModel)
        {
            var basket = await Get(userId);

            if (basket != null)
            {
                if (basket.BasketItems.All(item => item.ProductId != basketItemViewModel.ProductId))
                {
                    basket.BasketItems.Add(basketItemViewModel);
                }
            }
            else
            {
                basket = new BasketViewModel
                {
                    UserId = userId
                };
                basket.BasketItems.Add(basketItemViewModel);
            }

            await SaveOrUpdate(basket);
        }

        public async Task<Response<NoContent>> RemoveBasketItem(string userId, string productId)
        {
            var basket = await Get(userId);

            var deleteBasketItem = basket?.BasketItems.FirstOrDefault(item => item.ProductId == productId);

            if (deleteBasketItem == null)
            {
                return Response<NoContent>.Fail("No product was found in the cart according to the product code", 404);
            }

            var isDeleteBasket = basket.BasketItems.Remove(deleteBasketItem);

            if (!isDeleteBasket)
            {
                return Response<NoContent>.Fail("Could not remove product from basket", 500);
            }

            return await SaveOrUpdate(basket);
        }
        
        public async Task<Response<NoContent>> ComfirmBasket(ComfirmBasketDto comfirmBasketDto)
        {
            var basket = await Get(comfirmBasketDto.UserId);

            comfirmBasketDto.CargoPrice = basket.CargoPrice;
            comfirmBasketDto.TotalDiscountCargoPrice = basket.TotalDiscountCargoPrice;
            comfirmBasketDto.TotalDiscountPrice = basket.TotalDiscountPrice;
            comfirmBasketDto.TotalPrice = basket.TotalPrice;
            comfirmBasketDto.UsedCampaignId = basket.UsedCampaignId;

            basket.BasketItems.ForEach(item =>
            {
                comfirmBasketDto.OrderItems.Add(new OrderItem
                {
                    Price = item.CurrentPrice,
                    ProductCode = item.ProductCode,
                    ProductId = item.ProductId,
                    Quantity = item.Quantity
                });
            });

            var response = await _httpClient.PostAsJsonAsync<ComfirmBasketDto>("Baskets/ComfirmBasket", comfirmBasketDto);

            return response.IsSuccessStatusCode ? Response<NoContent>.Success((int)response.StatusCode) : Response<NoContent>.Fail("error", (int)response.StatusCode);
        }

        public async Task<Response<NoContent>> ApplyBasket(string userId, ApplyBasketDto applyBasketDto)
        {
            var campaignViewModel = await _campaignClient.GetByCampaignId(applyBasketDto.CampaignId);

            var basket = await Get(userId);

            basket.UsedCampaignId = campaignViewModel.Data.Id;

            if (basket.BasketItems.Sum(x=> x.Quantity) > campaignViewModel.Data.Threshold)
            {
                return Response<NoContent>.Fail("The campaign could not be implemented due to threshold", 500);
            }

            if(basket.HasCampaignAdded)
            {
                basket.RemoveCampaign();
            }

            switch (campaignViewModel.Data.CampaignType)
            {
                case "ORDER":
                    var discountPrice = CalculateDiscountPrice(basket.TotalPrice, campaignViewModel.Data.DiscountType,
                        campaignViewModel.Data.DiscountValue);
                    basket.TotalDiscountPrice = discountPrice;
                    break;
                case "CARGO":
                {
                    var discountCargoPrice = CalculateDiscountPrice(basket.CargoPrice, campaignViewModel.Data.DiscountType,
                        campaignViewModel.Data.DiscountValue);
                    basket.TotalDiscountCargoPrice = discountCargoPrice;
                    break;
                }
            }

            return await SaveOrUpdate(basket);
        }
        
        private static decimal CalculateDiscountPrice(decimal price, string discountType, decimal discountValue)
        {
            return discountType switch
            {
                "RATE" => price - (price * discountValue / 100),
                "AMOUNT" => price - discountValue,
                _ => price
            };
        }
        
    }
}
