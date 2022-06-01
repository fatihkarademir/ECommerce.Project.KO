using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Project.KO.Business.DTOs
{
    public class OrderDetailDto
    {
        public long OrderDetailId { get; set; }
        public long OrderId { get; set; }
        public long ProductId { get; set; }
        public int Count { get; set; }
        public OrderDto Order { get; set; }
    }
}
