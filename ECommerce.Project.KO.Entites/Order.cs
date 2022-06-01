using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Project.KO.Entities
{
    public class Order
    {
        public long OrderId { get; set; }
        public string UserId { get; set; }
        public ICollection<OrderDetail> OrderDetails { get; set; }
    }
}
