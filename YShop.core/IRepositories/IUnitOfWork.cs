using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YShop.core.IRepositories
{
    public interface IUnitOfWork
    {

        //props
        //ICountryRepository Country { get; }
        IProductRepository Product { get; }
        ITypeRepository Type { get; }
        IBrandRepository Brand { get; }
        

        void Save();
        

        Task SaveAsync();
        
    }
}
