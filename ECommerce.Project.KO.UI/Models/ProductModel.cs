using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerce.Project.KO.UI.Models
{
    public class ProductModel
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string ImageUrl { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public decimal DiscountRate { get; set; }
        //public List<Category> SelectedCategories { get; set; }
    }
}
