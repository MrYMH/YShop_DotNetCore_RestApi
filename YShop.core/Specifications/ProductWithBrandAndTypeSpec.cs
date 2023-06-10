using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YShop.core.Models;

namespace YShop.core.Specifications ////this class for each paerent class only
{
    public class ProductWithBrandAndTypeSpec : BaseSpecification<Product> 
    {
        public ProductWithBrandAndTypeSpec(ProductSpecParams param) : 
            base(x=>
                (string.IsNullOrEmpty(param.Search) || x.Name.ToLower().Contains(param.Search)) &&
                (!param.BrandId.HasValue || x.ProductBrandId == param.BrandId) &&
                (!param.TypeId.HasValue || x.ProductTypeId == param.TypeId)

            )
        {
            AddIncludes(x => x.ProductType);
            AddIncludes(x => x.ProductBrand);
            AddOrderBy(x => x.Name);
            ApplyPaging(param.PageSize *(param.PageIndex-1),param.PageSize);

            if (!string.IsNullOrEmpty(param.Sort))
            {
                switch(param.Sort)
                {
                    case "priceAsc":
                        AddOrderBy(p=>p.Price); 
                        break;

                    case "priceDesc":
                        AddOrderByDesc(p=>p.Price);
                        break;

                    default: 
                        AddOrderBy(p=>p.Name); 
                        break;

                }
            }

        }

        public ProductWithBrandAndTypeSpec(int id) :base(x=>x.Id == id )
        {
            AddIncludes(x => x.ProductType);
            AddIncludes(x => x.ProductBrand);
        }
    }
}
