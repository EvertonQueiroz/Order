using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Order.Infra.Data;
using Order.Domain.Interfaces.Data.Repositories;
using Order.Infra.Data.Repositories;
using Order.Infra.Data.UoW;
using Order.Domain.Interfaces.Data;
using Order.Domain.Interfaces.Commands.Handlers;
using Order.Domain.Commands.Handlers;
using Order.Domain.Interfaces.Queries.Handlers;
using Order.Domain.Queries.Handlers;
using Microsoft.Extensions.Logging;

namespace Order.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddControllers();
            services.AddTransient<ICreateOrUpdateOrderCommandHandler, CreateOrUpdateOrderCommandHandler>();
            services.AddTransient<IChangeStatusOrderCommandHandler, ChangeStatusOrderCommandHandler>();
            services.AddTransient<IDeleteOrderCommandHandler, DeleteOrderCommandHandler>();
            services.AddTransient<IFindAllOrdersQueryHandler, FindAllOrdersQueryHandler>();
            services.AddTransient<IFindOrderByNumberQueryHandler, FindOrderByNumberQueryHandler>();
            services.AddTransient<IOrderRepository, OrderRepository>();
            services.AddTransient<IUnitOfWork, UnitOfWork>();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Pedido.API", Version = "v1" });
            });

            services.AddDatabaseInfrastructure(Configuration);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Pedido.API v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

        }
    }
}
