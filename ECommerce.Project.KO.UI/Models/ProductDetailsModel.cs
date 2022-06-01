using ECommerce.Project.KO.Business.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerce.Project.KO.UI.Models
{
    public class ProductDetailsModel
    {
        public ProductDto Product { get; set; }
        public CategoryDto Category { get; set; }
        public long ProductId { get; set; }
        public CommentDto AddedComment { get; set; }
        public List<CommentDto> AllComments { get; set; }
    }
}
