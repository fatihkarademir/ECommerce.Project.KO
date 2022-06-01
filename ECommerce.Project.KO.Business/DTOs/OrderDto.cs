using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Project.KO.Business.DTOs
{
    public class OrderDto
    {
        public long OrderId { get; set; }
        public string UserId { get; set; }
        public ICollection<OrderDetailDto> OrderDetails { get; set; }
    }
}
