using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using YShop.core.Models;

namespace YShop.Infrastructure.Data
{
    public class StoreContextSeed
    {
        public static void seedAsync(StoreContext context)
        {
            if(!context.ProductBrands.Any())
            {
                var brandsData = File.ReadAllText("../YShop.Infrastructure/Data/SeedData/brands.json");
                var brands = JsonSerializer.Deserialize<List<ProductBrand>>(brandsData);
                context.ProductBrands.AddRange(brands);
                context.SaveChanges();
            }

            if (!context.ProductTypes.Any())
            {
                var typesData = File.ReadAllText("../YShop.Infrastructure/Data/SeedData/types.json");
                var types = JsonSerializer.Deserialize<List<ProductType>>(typesData);
                context.ProductTypes.AddRange(types);
                context.SaveChanges();

            }

            if (!context.Products.Any())
            {
                var ProductsData = File.ReadAllText("../YShop.Infrastructure/Data/SeedData/products.json");
                var products = JsonSerializer.Deserialize<List<Product>>(ProductsData);
                context.Products.AddRange(products);
                context.SaveChanges();

            }

        }
    }
}
