using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Shared.ControllerBase;
using Shared.Dtos;
using Web.Client.Abstract;
using Web.Models.Dtos;
using Web.Models.ViewModels;

namespace Web.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class BasketsController : CustomBaseController
    {
        private readonly IBasketClient _basketClient;
        private readonly IProductClient _productClient;

        public BasketsController(IBasketClient basketClient, IProductClient productClient)
        {
            _basketClient = basketClient;
            _productClient = productClient;
        }

        [HttpGet("{userId}")]
        public async Task<IActionResult> ComfirmBasket(string userId)
        {
            return CreateActionResultInstance(await _basketClient.ComfirmBasket(new ComfirmBasketDto { UserId = userId }));
        }

        [HttpGet("{campaignId}")]
        public async Task<IActionResult> ApplyBasket(string userId, int campaignId)
        {
            return CreateActionResultInstance(await _basketClient.ApplyBasket(userId, new ApplyBasketDto { CampaignId = campaignId }));
        }

        [HttpGet]
        public async Task<IActionResult> GetBasket(string userId)
        {
            var basketViewModel = await _basketClient.Get(userId);
            if(basketViewModel == null)
            {
                return BadRequest();
            }
            return Ok(basketViewModel);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteBasket(string userId)
        {
            return CreateActionResultInstance(await _basketClient.Delete(userId));
        }

        [HttpDelete("{productId}")]
        public async Task<IActionResult> RemoveBasketItem(string userId, string productId)
        {
            return CreateActionResultInstance(await _basketClient.RemoveBasketItem(userId, productId));
        }

        [HttpGet("{userId}/{productId}/{quantity}")]
        public async Task<IActionResult> AddBasketItem(string userId, string productId, int quantity)
        {
            var productViewModel = await _productClient.GetProductById(productId);

            if(productViewModel.Data.Stock - quantity < 0)
            {
                return CreateActionResultInstance(Response<NoContent>.Fail("not enough stock was found", 400));
            }

            BasketItemViewModel basketItem = new()
            {
                ProductId = productViewModel.Data.Id,
                ProductCode = productViewModel.Data.Code,
                Price = productViewModel.Data.Price,
                Quantity = quantity
            };

            await _basketClient.AddBasketItem(userId, basketItem);

            return CreateActionResultInstance(Response<NoContent>.Success(201));
        }
    }
}
