using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YShop.core.IRepositories;
using YShop.core.Models;
using YShop.Infrastructure.Data;

namespace YShop.Infrastructure.Repositories
{
    public class BrandRepository : GenericRepository<ProductBrand> , IBrandRepository
    {
        public BrandRepository(StoreContext context):base(context)
        {
                
        }
    }
}
