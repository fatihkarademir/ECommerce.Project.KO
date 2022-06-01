using ECommerce.Project.KO.DataAccess.Abstract;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Project.KO.DataAccess.Concrete.EntityFrameworkCore
{
    public class EfCoreGenericRepository<T, TContext> : IRepository<T>
        where T : class
        where TContext : DbContext,new()
    {
        public async Task<T> Create(T entity)
        {
            using (var context = new TContext())
            {
                await context.AddAsync(entity);
                await context.SaveChangesAsync();
                return entity;
            }
        }

        public async Task<int> Delete(long id)
        {
            using (var context = new TContext())
            {
                var entity = await GetById(id);
                context.Remove(entity);
                return context.SaveChanges();
            }
        }

        public ICollection<T> GetAllByFilterOrNotFiltered(Expression<Func<T, bool>> filter = null)
        {
            using (var context = new TContext())
            {
                return filter == null ? context.Set<T>().ToList()
                                      : context.Set<T>().Where(filter).ToList();
            }
        }

        public async Task<T> GetById(long id)
        {
            using (var context = new TContext())
            {
                return await context.Set<T>().FindAsync(id);
            }
        }

        public async Task<T> Update(T entity)
        {
            using (var context = new TContext())
            {
                context.Entry(entity).State = EntityState.Modified;
                await context.SaveChangesAsync();
                return entity;
            }
        }
    }
}
