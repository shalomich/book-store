using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using QueryWorker;
using QueryWorker.Visitors;
using Storage.Models;
using Storage.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Storage
{
    public class Startup
    {
        private IConfiguration _configuration;

        public Startup(IConfiguration configuration)
        {
            var buidler = new ConfigurationBuilder();
            _configuration = buidler.AddConfiguration(configuration).AddJsonFile("entityProperties.json").Build();
        }

        public void ConfigureServices(IServiceCollection services)
        {
            string connectionString = _configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<Database>(options => options.UseSqlServer(connectionString));
            
            services.AddControllers().AddNewtonsoftJson(options => options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);
            services.AddSingleton<IConfiguration>(_configuration);

            services.AddSingleton<EntityToFormConverter>();
            services.AddTransient<IQueryParser, QueryParser>();
            services.AddTransient(typeof(QueryTransformer<>));
            services.AddTransient(typeof(EntityConfig<>));
            services.AddTransient<EntityConfig<Publication>,PublicationConfig>();

            services.AddCors();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseCors(builder => builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
