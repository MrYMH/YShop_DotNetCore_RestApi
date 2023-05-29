using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using YShop.core.Models;
using YShop.core.Specifications;

namespace YShop.core.IRepositories
{
    public interface IGenericRepository<T> where T:BaseEntity
    {
        Task<T> GetByIdAsync(Expression<Func<T, bool>> filter);
        Task<List<T>> GetAllAsync();
        Task<T> GetFirstAsync(Expression<Func<T, bool>> filter , string includeprops);

        //functions with specification pattern:
        Task<T> GetEntityWithSpecAsync(ISpecification<T> spec);
        Task<List<T>> ListAllWithSpecAsync(ISpecification<T> spec);

        Task<int> CountAsync(ISpecification<T> spec);
    }
}
