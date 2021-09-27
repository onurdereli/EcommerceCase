using System.Threading.Tasks;
using Basket.Models.Dtos;
using Shared.Dtos;

namespace Basket.Services.Abstract
{
    public interface IBasketService
    {
        Task<Response<BasketDto>> GetBasketAysnc(string userId);

        Task<Response<NoContent>> SaveOrUpdateAysnc(BasketDto basketDto);

        Task<Response<NoContent>> DeleteAysnc(string userId);

        Task<Response<NoContent>> ComfirmBasket(ComfirmBasketDto comfirmBasketDto);
    }
}
