using System;
using System.Text.Json;
using System.Threading.Tasks;
using AutoMapper;
using Basket.Models.Dtos;
using Basket.Services.Abstract;
using Shared.Dtos;
using Shared.Messages.Events.Abstract;
using Shared.Messages.Events.Concrete;
using MassTransit;
using StackExchange.Redis;

namespace Basket.Services.Concrete
{
    public class BasketService : IBasketService
    {
        private readonly RedisService _redisService;
        private readonly IMapper _mapper;
        private readonly IPublishEndpoint _publishEndpoint;

        public BasketService(RedisService redisService, IMapper mapper, IPublishEndpoint publishEndpoint)
        {
            _redisService = redisService;
            _mapper = mapper;
            _publishEndpoint = publishEndpoint;
        }

        public async Task<Shared.Dtos.Response<BasketDto>> GetBasketAysnc(string userId)
        {
            RedisValue existBasket;
            try
            {
                existBasket = await _redisService.GetDb().StringGetAsync(userId);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

            if (existBasket.IsNullOrEmpty)
            {
                return Shared.Dtos.Response<BasketDto>.Fail("Basket not found", 404);
            }

            return Shared.Dtos.Response<BasketDto>.Success(JsonSerializer.Deserialize<BasketDto>(existBasket), 200);
        }

        public async Task<Shared.Dtos.Response<NoContent>> SaveOrUpdateAysnc(BasketDto basketDto)
        {
            var status = await _redisService.GetDb().StringSetAsync(basketDto.UserId, JsonSerializer.Serialize(basketDto));

            return status ? Shared.Dtos.Response<NoContent>.Success(204) : Shared.Dtos.Response<NoContent>.Fail("Basket could not update or save", 500);
        }

        public async Task<Shared.Dtos.Response<NoContent>> DeleteAysnc(string userId)
        {
            var status = await _redisService.GetDb().KeyDeleteAsync(userId);

            return status ? Shared.Dtos.Response<NoContent>.Success(204) : Shared.Dtos.Response<NoContent>.Fail("Basket not found", 404);
        }

        public async Task<Shared.Dtos.Response<NoContent>> ComfirmBasket(ComfirmBasketDto comfirmBasketDto)
        {
            var comfirmBasketEventMessage = _mapper.Map<BasketComfirmedEvent>(comfirmBasketDto);

            await _publishEndpoint.Publish<IBasketComfirmedEvent>(comfirmBasketEventMessage);

            return Shared.Dtos.Response<NoContent>.Success(204);
        }
    }
}