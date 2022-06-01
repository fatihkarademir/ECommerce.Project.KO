using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerce.Project.KO.UI.Dtos
{
    public class BasketDto
    {
        public string UserId { get; set; }
        public decimal DiscountRate { get; set; }
        public List<BasketItemDto> basketItems { get; set; }

        public decimal GetTotalPrice()
        {
            var totalPrice =  basketItems.Sum(x => x.Price * x.Quantity);
            return totalPrice - ((totalPrice * DiscountRate)/100);
        }
    }
}
