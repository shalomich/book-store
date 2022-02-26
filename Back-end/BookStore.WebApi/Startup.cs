
using BookStore.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using QueryWorker.Extensions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookStore.Persistance;
using BookStore.Application.Services;
using BookStore.Application.Services.DbQueryBuilders;
using System.Reflection;
using BookStore.WebApi.Attributes.GenericController;
using BookStore.WebApi.Middlewares;
using BookStore.Application.Providers;
using Microsoft.AspNetCore.Identity;

namespace BookStore.WebApi
{
    public class Startup
    {
        private IConfiguration _configuration;

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            var applicationAssembly = AppDomain.CurrentDomain.GetAssemblies()
                .Single(assebmly => assebmly.GetName().Name == "BookStore.Application");
            string connectionString = _configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<ApplicationContext>(options =>
            {
                options.UseSqlServer(connectionString, 
                    builder => builder.MigrationsAssembly("BookStore.Persistance"));
            });

            services.AddControllers()
                .AddNewtonsoftJson(options => options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore)
                .ConfigureApplicationPartManager(options => options.FeatureProviders.Add(new GenericControllerFeatureProvider())); ;

            services.AddScoped<JwtParser>();
            services.AddScoped<TokensFactory>();
            services.AddScoped<RefreshTokenRepository>();
            services.AddScoped<LoggedUserAccessor>();

            services.AddDataTransformerBuildFacade(applicationAssembly);
            services.AddScoped(typeof(DbEntityQueryBuilder<>));
            services.AddScoped(typeof(DbFormEntityQueryBuilder<>));

            var jwtSettings = _configuration.GetSection("JwtSettings").Get<JwtSettings>();
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.TokenKey));

            services.Configure<DataProtectionTokenProviderOptions>(options => 
                options.TokenLifespan = TimeSpan.FromMinutes(15));

            services.AddIdentity<User, IdentityRole<int>>()
                .AddEntityFrameworkStores<ApplicationContext>()
                .AddTokenProvider(_configuration["AppTokenProvider"], typeof(DataProtectorTokenProvider<User>));
            
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(
                opt =>
                {
                    opt.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = securityKey,
                        ValidateAudience = false,
                        ValidateIssuer = false,
                    };
                });

            services.AddMediatR(applicationAssembly);
            services.AddAutoMapper(Assembly.GetExecutingAssembly(), applicationAssembly);
            services.AddCors();

            services.AddSwaggerGen();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

           

            app.UseRouting();

            app.UseMiddleware<ErrorHandlingMiddleware>();

            app.UseAuthentication();
            app.UseAuthorization();
            
            app.UseCors(builder => builder
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader()
                .WithExposedHeaders("dataCount"));

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            });
        }
    }
}
