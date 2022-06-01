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
    public class EfCoreProductDAL : EfCoreGenericRepository<Product, ECommerceDbContext>, IProductDAL
    {
        public async Task<Product> GetDetailById(long id)
        {
            using (var context = new ECommerceDbContext())
            {
                return await context.Products
                             .Where(i => i.ProductId == id)
                             .Include(i => i.Category).FirstOrDefaultAsync();
            }
        }
    }
}
