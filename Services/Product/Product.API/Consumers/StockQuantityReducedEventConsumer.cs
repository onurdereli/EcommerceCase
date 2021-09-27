using System.Threading.Tasks;
using AutoMapper;
using Product.Models.Requests;
using Product.Services.Abstract;
using Shared.Messages.Events.Abstract;
using MassTransit;
using Microsoft.Extensions.Logging;

namespace Product.API.Consumers
{
    public class StockQuantityReducedEventConsumer: IConsumer<IStockQuantityReducedEvent>
    {
        private readonly IMapper _mapper;
        private readonly ILogger<StockQuantityReducedEventConsumer> _logger;
        private readonly IProductService _productService;

        public StockQuantityReducedEventConsumer(IMapper mapper, ILogger<StockQuantityReducedEventConsumer> logger, IProductService productService)
        {
            _mapper = mapper;
            _logger = logger;
            _productService = productService;
        }

        public async Task Consume(ConsumeContext<IStockQuantityReducedEvent> context)
        {
            var product = await _productService.GetProductById(context.Message.Id);

            UpdateProductRequest updateProductRequest = new()
            {
                Id = context.Message.Id,
                Code = context.Message.Code,
                Price = context.Message.Price,
                Stock = product.Data.Stock - context.Message.Quantity
            };

            await _productService.Update(updateProductRequest);

            _logger.LogInformation("StockQuantityReducedEventConsumer consumed successfully.");
        }
    }
}
