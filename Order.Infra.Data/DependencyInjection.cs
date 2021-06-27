using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Order.Infra.Data.Context;

namespace Order.Infra.Data
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddDatabaseInfrastructure(
           this IServiceCollection services,
           IConfiguration configuration)
        {
            services.AddDbContext<OrderDBContext>(
                options => options.UseSqlite(
                    configuration.GetConnectionString("OrderConnectionString")
                    ));

            services.AddScoped<OrderDBContext, OrderDBContext>();

            return services;
        }
    }
}
