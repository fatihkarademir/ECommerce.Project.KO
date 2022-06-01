using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Project.KO.DataAccess.Abstract
{
    public interface IRepository<T>
    {
        Task<T> Create(T entity);
        Task<T> Update(T entity);
        Task<int> Delete(long id);
        ICollection<T> GetAllByFilterOrNotFiltered(Expression<Func<T, bool>> filter = null);
        Task<T> GetById(long id);
    }
}
