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
            LoadData(serviceProvider.GetService<ApiContext>());
        }

        #region Private Methods

        /// <summary>
        /// Function to load the Equities
        /// </summary>
        /// <param name="context">Api Context</param>
        private void LoadData(ApiContext context)
        {
            context.Equities.Add(new Equity { Id = 1, EquityName = "HIL", Price = 42.11 });
            context.Equities.Add(new Equity { Id = 2, EquityName = "ITC", Price = 202.43 });
            context.Equities.Add(new Equity { Id = 3, EquityName = "TCS", Price = 321.21 });
            context.Equities.Add(new Equity { Id = 4, EquityName = "India Bulls", Price = 1020.21 });
            context.Equities.Add(new Equity { Id = 5, EquityName = "HDFC Bank", Price = 1522.35 });
            context.Equities.Add(new Equity { Id = 6, EquityName = "PNB", Price = 40.75 });
            context.Equities.Add(new Equity { Id = 7, EquityName = "Reliance", Price = 2500.54 });

            context.TraderFunds.Add(new TraderFund { Id = 1, RemainingBalance = 0 });

            context.SaveChanges();
        }

        #endregion
    }
}
