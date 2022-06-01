using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Project.KO.Business.DTOs
{
    public class CategoryDto
    {
        public long CategoryId { get; set; }
        public string CategoryName { get; set; }
        public ICollection<ProductDto> ProductDtos { get; set; }
    }
}
