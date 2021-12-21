using eBroker.Repository;
using eBroker.Repository.Implementation;
using eBroker.Repository.Interface;
using eBroker.Repository.Model;
using eBroker.Service.Implementation;
using eBroker.Service.Interface;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eBroker
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
            services.AddScoped<IEquityRepository, EquityRepository>();
            services.AddScoped<ITraderEquityRepository, TraderEquityRepository>();
            services.AddScoped<ITraderFundRepository, TraderFundRepository>();
            services.AddDbContext<ApiContext>(opt => opt.UseInMemoryDatabase(databaseName: "eBroker"));
            services.AddScoped<ApiContext>();

            services.AddTransient<ITraderEquityService, TraderEquityService>();
            services.AddTransient<ITradeFundService, TradeFundService>();

            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IServiceProvider serviceProvider)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            // Load initial Equities
            serviceProvider.GetService<ApiContext>().LoadData();
        }
    }
}
