using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Project.KO.Business.DTOs
{
    public class CommentDto
    {
        public long CommentId { get; set; }
        public string CommentDescription { get; set; }
        public string UserId { get; set; }
        public long ProductId { get; set; }
        public ProductDto Product { get; set; }
        public IdentityUser UserDto { get; set; }
    }
}
