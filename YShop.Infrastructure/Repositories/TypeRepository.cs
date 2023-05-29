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
    public class TypeRepository:GenericRepository<ProductType> , ITypeRepository
    {
        public TypeRepository(StoreContext context):base(context)
        {
        }
    }
}
