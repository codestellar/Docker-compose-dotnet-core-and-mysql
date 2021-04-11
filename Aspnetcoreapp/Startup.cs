using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ProductLibrary;
using ProductLibrary.Config;

namespace aspnetcoreapp
{
    public class Startup
    {
        public Startup(IConfiguration configuration) => Configuration = configuration;

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();

            services.AddSingleton(GetDbConfig());
            services.AddSingleton<IProductsProvider, ProductsProvider>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.EnvironmentName == "Development") 
                app.UseDeveloperExceptionPage();
            app.UseRouting();
            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }

        private DbConfig GetDbConfig()
        {
            string server = Environment.GetEnvironmentVariable("DB_SERVER") ?? "localhost";
            string dbName = Environment.GetEnvironmentVariable("DB_NAME") ?? "product-db";
            string port = Environment.GetEnvironmentVariable("DB_PORT") ?? "3306";
            string user = Environment.GetEnvironmentVariable("DB_USER") ?? "root";
            string password = Environment.GetEnvironmentVariable("DB_PASSWORD") ?? "myPass";

            return new DbConfig
            {
                ConnectionString = $"Server=db;Port={port};Database={dbName};Uid={user}; Pwd={password};"
            };
        }
    }
}
