using System.Threading.Tasks;
using Shared.Dtos;
using Web.Models.Dtos;
using Web.Models.ViewModels;

namespace Web.Client.Abstract
{
    public interface IBasketClient
    {
        Task<BasketViewModel> Get(string userId);

        Task<Response<NoContent>> Delete(string userId);

        Task AddBasketItem(string userId, BasketItemViewModel basketItemViewModel);

        Task<Response<NoContent>> RemoveBasketItem(string userId, string productId);

        Task<Response<NoContent>> ApplyBasket(string userId, ApplyBasketDto applyBasketDto);

        Task<Response<NoContent>> ComfirmBasket(ComfirmBasketDto comfirmBasketDto);
    }
}
