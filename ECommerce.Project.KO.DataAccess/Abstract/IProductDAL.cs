using ECommerce.Project.KO.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Project.KO.DataAccess.Abstract
{
    public interface IProductDAL : IRepository<Product>
    {
        Task<Product> GetDetailById(long id);
    }
}
