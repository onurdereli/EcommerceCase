using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Web.Models.ViewModels
{
    public class OrderViewModel
    {
        public int Id { get; set; }

        public string UserId { get; set; }

        public decimal TotalPrice { get; set; }

        public decimal TotalDiscountPrice { get; set; }

        public decimal CargoPrice { get; set; }

        public decimal TotalDiscountCargoPrice { get; set; }
    }
}
