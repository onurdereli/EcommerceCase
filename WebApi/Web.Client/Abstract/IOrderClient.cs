using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared.Dtos;
using Web.Models.ViewModels;

namespace Web.Client.Abstract
{
    public interface IOrderClient
    {
        Task<Response<IEnumerable<OrderViewModel>>> GetOrdersByUserId(string userId);
    }
}
