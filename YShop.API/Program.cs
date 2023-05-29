
using Microsoft.EntityFrameworkCore;
using YShop.API.Helpers;
using YShop.API.Middleware;
using YShop.core.IRepositories;
using YShop.Infrastructure.Data;
using YShop.Infrastructure.Repositories;

namespace YShop.API
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            //add db context
            builder.Services.AddDbContext<StoreContext>(options =>
            {
                options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection"));
            });

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            //set repository pattern scope
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

            //set AutoMapper
            builder.Services.AddAutoMapper(typeof(MappingConfig));

            //add Cors
            builder.Services.AddCors(opt =>
            {
                opt.AddPolicy("CorsPolicy", policy =>
                {
                    policy.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin();
                });
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            //error handling with exception
            app.UseMiddleware<ExceptionMiddleware>();
            app.UseStatusCodePagesWithReExecute("/errors/{0}");

            app.UseHttpsRedirection();
            
            // use static files middleware
            app.UseStaticFiles();

            //Use Cors
            app.UseCors("CorsPolicy");

            app.UseAuthorization();


            app.MapControllers();

            using (var scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var context = services.GetRequiredService<StoreContext>();
                var logger = services.GetRequiredService<ILogger<Program>>();

                try
                {
                    await context.Database.MigrateAsync();
                    StoreContextSeed.seedAsync(context);
                }
                catch (Exception ex)
                {
                    logger.LogError(ex, "error occured while migrating process");
                }
            }
            


            app.Run();
        }
    }
}