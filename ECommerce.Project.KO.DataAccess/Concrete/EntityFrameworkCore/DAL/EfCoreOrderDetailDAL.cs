using ECommerce.Project.KO.DataAccess.Abstract;
using ECommerce.Project.KO.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Project.KO.DataAccess.Concrete.EntityFrameworkCore.DAL
{
    public class EfCoreOrderDetailDAL : EfCoreGenericRepository<OrderDetail, ECommerceDbContext>, IOrderDetailDAL
    {
    }
}
