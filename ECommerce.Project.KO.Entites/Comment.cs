using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Project.KO.Entities
{
    public class Comment
    {
        public long CommentId { get; set; }
        public string CommentDescription { get; set; }
        public string UserId { get; set; }
        public long ProductId { get; set; }
        public Product Product { get; set; }
    }
}
