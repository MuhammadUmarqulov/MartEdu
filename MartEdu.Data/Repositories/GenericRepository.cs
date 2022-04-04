using MartEdu.Data.Contexts;
using MartEdu.Data.IRepositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;


namespace MartEdu.Data.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        internal MartEduDbContext dbContext;
        internal DbSet<T> dbSet;

        public GenericRepository(MartEduDbContext dbContext)
        {
            this.dbContext = dbContext;
            dbSet = dbContext.Set<T>();
        }


        public async Task<T> CreateAsync(T entity)
        {
            var res = await dbSet.AddAsync(entity);

            return res.Entity;
        }

        public async Task<bool> DeleteAsync(Expression<Func<T, bool>> expression)
        {
            var entity = await dbSet.FirstOrDefaultAsync(expression);

            if (entity is null)
                return false;

            dbSet.Remove(entity);

            return true;
        }

        public async Task<T> GetAsync(Expression<Func<T, bool>> expression)
        {
            return (await WhereAsync(expression)).FirstOrDefault();
        }

        public async Task<T> UpdateAsync(T entity) => dbSet.Update(entity).Entity;

        public async Task<IQueryable<T>> WhereAsync(Expression<Func<T, bool>> expression = null)
        {
            return expression is null ? dbSet : dbSet.Where(expression);
        }
    }
}