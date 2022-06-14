
using BookStore.Domain.Entities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Text;
using BookStore.Persistance;
using BookStore.WebApi.Middlewares;
using Microsoft.AspNetCore.Identity;
using BookStore.Application.Extensions;
using Hangfire;
using Newtonsoft.Json;
using BookStore.Persistance.Extensions;
using BookStore.WebApi.BackgroundJobs.BookEditing;
using BookStore.WebApi.BackgroundJobs.Selection;
using Hangfire.PostgreSql;
using BookStore.Application.Commands.Account.Common;
using Microsoft.Extensions.Logging;

namespace BookStore.WebApi
{
    public class Startup
    {
        private IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            var currentAssemblyName = typeof(Startup).Assembly;

            services.AddPersistance(Configuration);
            services.AddApplicationCore(Configuration, currentAssemblyName);

            services.AddControllers()
                .AddNewtonsoftJson(options => options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Auth:Jwt:Web:TokenKey"]));

            services.Configure<DataProtectionTokenProviderOptions>(options =>
                options.TokenLifespan = TimeSpan.FromMinutes(Configuration.GetSection("Auth:RefreshTokenExpiredMinutes").Get<int>()));

            services.AddIdentity<User, IdentityRole<int>>()
                .AddEntityFrameworkStores<ApplicationContext>()
                .AddTokenProvider(Configuration["Auth:AppTokenProvider"], typeof(DataProtectorTokenProvider<User>))
                .AddClaimsPrincipalFactory<ApplicationPrincipalFactory>();

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(
                opt =>
                {
                    opt.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = securityKey,
                        ValidateAudience = false,
                        ValidateIssuer = false,
                        ClockSkew = TimeSpan.Zero
                    };
                });

            services.Configure<IdentityOptions>(options =>
            {
                options.Password.RequireUppercase = false;
                options.Password.RequireDigit = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireLowercase = false;
                options.User.AllowedUserNameCharacters = "";
                options.Password.RequiredLength = 8;
                options.Password.RequireDigit = true;
                options.User.RequireUniqueEmail = true;
            });

            services.AddLogging(builder =>
            {
                builder.AddConsole();
                builder.AddConfiguration(Configuration);
            });

            services.AddCors();

            services.AddSwaggerGen();

            ConfigureHangfire(services);
        }

        private void ConfigureHangfire(IServiceCollection services)
        {
            var connectionString = Configuration.GetConnectionString("DefaultConnection");

            services.AddHangfire(configuration => configuration
               .SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
               .UseSimpleAssemblyNameTypeSerializer()
               .UseRecommendedSerializerSettings()
               .UseSerializerSettings(new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore })
               .UsePostgreSqlStorage(connectionString, new PostgreSqlStorageOptions
                {
                    InvisibilityTimeout = TimeSpan.FromDays(1)
                }));

            services.AddHangfireServer();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            Configuration[nameof(env.ContentRootPath)] = env.ContentRootPath;

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

            RunBackgroundJobs();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHangfireDashboard();
            });

            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            });
        }

        private static void RunBackgroundJobs()
        {
            var utcHour = 24;
            var hourDifference = 7;
            var localHour = utcHour - hourDifference;

            RecurringJob.AddOrUpdate<RemoveDiscountJob>(job => job.RemoveDiscount(default), Cron.Daily(localHour));
            RecurringJob.AddOrUpdate<ChooseCurrentDayAuthorJob>(job => job.ChooseCurrentDayAuthor(default), Cron.Daily(localHour));
        }
    }
}
