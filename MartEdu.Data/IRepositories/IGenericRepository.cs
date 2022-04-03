using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;


namespace MartEdu.Data.IRepositories
{
    public interface IGenericRepository <T> where T : class 
    {
        Task<T> CreateAsync(T entity);
        Task<T> UpdateAsync(T entity);
        Task<bool> DeleteAsync(Expression<Func<T, bool>> expression);
        Task<T> GetAsync(Expression<Func<T, bool>> expression);
        Task<IQueryable<T>> WhereAsync(Expression<Func<T, bool>> expression = null);
    }
}
