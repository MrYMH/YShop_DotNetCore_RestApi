using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YShop.core.Models;

namespace YShop.core.Specifications
{
    public class ProductWithFiltersForCountSepecification : BaseSpecification<Product>
    {
        public ProductWithFiltersForCountSepecification(ProductSpecParams productParams)
            : base(x =>
             (string.IsNullOrEmpty(productParams.Search) || x.Name.ToLower().Contains(productParams.Search)) &&
             (!productParams.BrandId.HasValue || x.ProductBrandId == productParams.BrandId) &&
             (!productParams.TypeId.HasValue || x.ProductTypeId == productParams.TypeId))

        {

        }
    }
}
