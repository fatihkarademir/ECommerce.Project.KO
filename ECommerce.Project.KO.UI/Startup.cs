using ECommerce.Project.KO.Business.Abstract;
using ECommerce.Project.KO.Business.Concrete;
using ECommerce.Project.KO.DataAccess.Abstract;
using ECommerce.Project.KO.DataAccess.Concrete.EntityFrameworkCore;
using ECommerce.Project.KO.DataAccess.Concrete.EntityFrameworkCore.DAL;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ECommerce.Project.KO.UI.Identity;
using Microsoft.AspNetCore.Http;
using ECommerce.Project.KO.UI.Settings;
using ECommerce.Project.KO.UI.Services;
using Microsoft.Extensions.Options;

namespace ECommerce.Project.KO.UI
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
            #region DI
            services.AddScoped<IAddressDAL, EfCoreAddressDAL>();
            services.AddScoped<ICategoryDAL, EfCoreCategoryDAL>();
            services.AddScoped<ICommentDAL, EfCoreCommentDAL>();
            services.AddScoped<IOrderDAL, EfCoreOrderDAL>();
            services.AddScoped<IOrderDetailDAL, EfCoreOrderDetailDAL>();
            services.AddScoped<IProductDAL, EfCoreProductDAL>();

            services.AddScoped<IAddressService, AddressService>();
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<ICommentService, CommentService>();
            services.AddScoped<IOrderDetailService, OrderDetailService>();
            services.AddScoped<IOrderService, OrderService>();
            services.AddScoped<IProductService, ProductService>();

            services.AddScoped<IBasketService, BasketService>();
            #endregion

            #region Redis Settings

            services.Configure<RedisSettings>(Configuration.GetSection("RedisSettings"));
            services.AddSingleton<RedisService>(sp =>
            {
                var redisSettings = sp.GetRequiredService<IOptions<RedisSettings>>().Value;

                var redis = new RedisService(redisSettings.Host, redisSettings.Port);

                redis.Connect();
                return redis;
            });
            #endregion


            #region Db Context
            services.AddDbContext<ECommerceDbContext>();
            services.AddDbContext<AppIdentityDbContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("SqlServer"), sqlOptions =>
                 {

                 });
            });

            #endregion

            #region Identity
            services.AddIdentity<IdentityUser, IdentityRole>(opt =>
            {
                opt.Password.RequiredLength = 5;
                opt.Password.RequireNonAlphanumeric = false;
                opt.Password.RequireLowercase = false;
                opt.Password.RequireUppercase = false;

                opt.User.RequireUniqueEmail = true;
            })
            .AddEntityFrameworkStores<AppIdentityDbContext>()
            .AddDefaultTokenProviders();



            #endregion

            #region Cookie
            services.ConfigureApplicationCookie(opt =>
            {
                opt.LoginPath = new PathString("/Account/Login");
                opt.LogoutPath = new PathString("/User/Logout");
                opt.Cookie = new CookieBuilder
                {
                    Name = "AspNetCoreIdentityCookie",
                    HttpOnly = false
                };
                opt.SlidingExpiration = true;
                opt.ExpireTimeSpan = TimeSpan.FromMinutes(10);
                opt.AccessDeniedPath = new PathString("/authority/page");
            });
            #endregion
            services.AddControllersWithViews();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                SeedDatabase.Seed();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseStaticFiles();

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Shop}/{action=List}/{id?}");
            });
        }
    }
}
