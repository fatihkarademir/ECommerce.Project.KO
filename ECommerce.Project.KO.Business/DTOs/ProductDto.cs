using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Project.KO.Business.DTOs
{
    public class ProductDto
    {
        public long ProductId { get; set; }
        public string ProductName { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public decimal Price { get; set; }
        public decimal DiscountRate { get; set; }
        public string Color { get; set; }
        public string Size { get; set; }
        public long CategoryId { get; set; }
        public CategoryDto Category { get; set; }
        public ICollection<CommentDto> Comments { get; set; }
    }
}
