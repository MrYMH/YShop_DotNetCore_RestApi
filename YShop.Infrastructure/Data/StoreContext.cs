using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using YShop.core.Models;

namespace YShop.Infrastructure.Data
{
    public class StoreContext:DbContext
    {
        public StoreContext(DbContextOptions<StoreContext> options):base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            //sqlite does not support decimal datatype 
            //convert decimal to double
            if(Database.ProviderName == "Microsoft.EntityFrameworkcore.Sqlite")
            {
                foreach(var entitytype in modelBuilder.Model.GetEntityTypes())
                {
                    var props = entitytype.ClrType.GetProperties().Where(p=>p.PropertyType == typeof(decimal));
                    foreach(var property in props)
                    {
                        modelBuilder.Entity(entitytype.Name).Property(property.Name).HasConversion<double>();
                    }
                }
            }
        }

        //tables here:
        // public DbSet<Country> Countries { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductType> ProductTypes { get; set; }
        public DbSet<ProductBrand> ProductBrands { get; set; }



    }
}
