using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using YShop.core.IRepositories;
using YShop.core.Models;
using YShop.core.Specifications;
using YShop.Infrastructure.Data;

namespace YShop.Infrastructure.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {
        private readonly StoreContext _context;

        public GenericRepository(StoreContext context)
        {
            this._context = context;
        }
        //ActionResult<IReadOnlyList<T>>
        public async Task<List<T>> GetAllAsync()
        {
            return await _context.Set<T>().ToListAsync();
        }

        public async Task<T> GetByIdAsync(Expression<Func<T, bool>> filter = null)
        {
            return await _context.Set<T>().Where(filter).FirstOrDefaultAsync();

        }

        

        public async Task<T> GetFirstAsync(Expression<Func<T, bool>> filter = null, string includeprops = null)
        {
            var dbset = _context.Set<T>().Where(filter);
            if (includeprops != null)
            {
                foreach (var includeProp in includeprops.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    dbset = dbset.Include(includeProp);
                }
            }
            var query = await dbset.FirstOrDefaultAsync();
            return query;
        }


        // refactor after adding specification pattern

        public async Task<T> GetEntityWithSpecAsync(ISpecification<T> spec)
        {
            return await ApplaySpecification(spec).FirstOrDefaultAsync();
        }

        public async Task<List<T>> ListAllWithSpecAsync(ISpecification<T> spec)
        {
            return await ApplaySpecification(spec).ToListAsync();
        }

        private IQueryable<T> ApplaySpecification (ISpecification<T> spec)
        {
            return SpecificationEvaluator<T>.GetQuery(_context.Set<T>().AsQueryable(), spec);
        }

        public async Task<int> CountAsync(ISpecification<T> spec)
        {
            return await ApplaySpecification(spec).CountAsync();
        }
    }
    
}
