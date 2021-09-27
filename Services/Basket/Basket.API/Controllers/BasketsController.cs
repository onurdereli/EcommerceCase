using System.Linq;
using System.Threading.Tasks;
using Basket.Models.Dtos;
using Basket.Services.Abstract;
using Shared.ControllerBase;
using Shared.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace Basket.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BasketsController : CustomBaseController
    {
        private readonly IBasketService _basketService;

        public BasketsController(IBasketService basketService)
        {
            _basketService = basketService;
        }

        [HttpGet("{userId}")]
        public async Task<IActionResult> GetBasket(string userId)
        {
            return CreateActionResultInstance(await _basketService.GetBasketAysnc(userId));
        }

        [HttpPost]
        [Route("[action]", Name = "SaveOrUpdateBasket")]
        public async Task<IActionResult> SaveOrUpdateBasket(BasketDto basketDto)
        {
            return CreateActionResultInstance(await _basketService.SaveOrUpdateAysnc(basketDto));
        }

        [HttpDelete("{userId}")]
        public async Task<IActionResult> DeleteBasket(string userId)
        {
            return CreateActionResultInstance(await _basketService.DeleteAysnc(userId));
        }

        [HttpPost]
        [Route("[action]", Name = "ComfirmBasket")]
        public async Task<IActionResult> ComfirmBasket([FromBody] ComfirmBasketDto comfirmBasketDto)
        {
            return CreateActionResultInstance(await _basketService.ComfirmBasket(comfirmBasketDto));
        }
    }
}
