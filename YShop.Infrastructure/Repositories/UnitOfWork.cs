using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YShop.core.IRepositories;
using YShop.Infrastructure.Data;

namespace YShop.Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly StoreContext _context;

        //my repos
        public IProductRepository Product { get; private set; }
        public IBrandRepository Brand { get; private set; }
        public ITypeRepository Type { get; private set; }

        public UnitOfWork(StoreContext context)
        {
            this._context = context;
            Product = new ProductRepository(_context);
            Brand = new BrandRepository(_context);
            Type = new TypeRepository(_context);
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
        

    }
}
