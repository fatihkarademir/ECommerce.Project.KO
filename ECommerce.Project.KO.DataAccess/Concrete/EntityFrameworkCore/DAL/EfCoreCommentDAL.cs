using ECommerce.Project.KO.DataAccess.Abstract;
using ECommerce.Project.KO.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Project.KO.DataAccess.Concrete.EntityFrameworkCore.DAL
{
    public class EfCoreCommentDAL : EfCoreGenericRepository<Comment, ECommerceDbContext>, ICommentDAL
    {
        public async Task<List<Comment>> GetCommentsByProductId(long productId)
        {
            using (var context = new ECommerceDbContext())
            {
                return await context.Comments
                             .Where(i => i.ProductId == productId)
                             .Include(i => i.Product).ToListAsync();
            }

        }
    }
}
