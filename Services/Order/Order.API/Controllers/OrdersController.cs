using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Order.Application.Queries;
using Shared.ControllerBase;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Order.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : CustomBaseController
    {
        private readonly IMediator _mediator;
        private readonly ILogger<OrdersController> _logger;

        public OrdersController(IMediator mediator, ILogger<OrdersController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }
        
        [HttpGet("{userId}")]
        public async Task<IActionResult> GetOrders(string userId)
        {
            return CreateActionResultInstance(await _mediator.Send(new GetOrdersByUserIdQuery(userId)));
        }
    }
}
