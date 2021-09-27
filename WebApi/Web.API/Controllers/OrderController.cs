using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Shared.ControllerBase;
using Web.Client.Abstract;

namespace Web.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : CustomBaseController
    {
        private readonly IOrderClient _orderClient;

        public OrderController(IOrderClient orderClient)
        {
            _orderClient = orderClient;
        }

        [HttpGet("{userId}")]
        public async Task<IActionResult> GetOrdersByUserId(string userId)
        {
            return CreateActionResultInstance(await _orderClient.GetOrdersByUserId(userId));
        }
    }
}
