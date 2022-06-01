using ECommerce.Project.KO.DataAccess.Concrete.EntityFrameworkCore;
using ECommerce.Project.KO.Entities;
using ECommerce.Project.KO.UI.Identity;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerce.Project.KO.UI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            try
            {
                var host = CreateHostBuilder(args).Build();
                using (var scope = host.Services.CreateScope())
                {
                    var serviceProvider = scope.ServiceProvider;

                    var appIdentityDbContext = serviceProvider.GetRequiredService<AppIdentityDbContext>();
                    var eCommerceDbContext = serviceProvider.GetRequiredService<ECommerceDbContext>();
                    var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
                    var userManager = serviceProvider.GetRequiredService<UserManager<IdentityUser>>();
                    appIdentityDbContext.Database.Migrate();
                    eCommerceDbContext.Database.Migrate();

                    if (!eCommerceDbContext.Categories.Any())
                    {
                        eCommerceDbContext.Categories.AddRange(new List<Category>()
                        {
                             new Category() {CategoryName="Telefon"},
                             new Category() {CategoryName="Bilgisayar"},
                             new Category() {CategoryName="Elektronik"}
                        });
                        eCommerceDbContext.SaveChanges();
                        eCommerceDbContext.Products.AddRange(new List<Product>()
                        {
                            new Product(){ProductName="Samsung S5",Price=2000,ImageUrl="1.jpg",Description="<p>Bu bir açýklamadýr.</p>",CategoryId=eCommerceDbContext.Categories.FirstOrDefault().CategoryId},
                            new Product(){ProductName="Samsung S6",Price=3000,ImageUrl="1.jpg",Description="<p>Bu bir açýklamadýr.</p>",CategoryId=eCommerceDbContext.Categories.FirstOrDefault().CategoryId},
                            new Product(){ProductName="Samsung S7",Price=4000,ImageUrl="1.jpg",Description="<p>Bu bir açýklamadýr.</p>",CategoryId=eCommerceDbContext.Categories.FirstOrDefault().CategoryId},
                            new Product(){ProductName="Samsung S8",Price=5000,ImageUrl="1.jpg",Description="<p>Bu bir açýklamadýr.</p>",CategoryId=eCommerceDbContext.Categories.FirstOrDefault().CategoryId},
                            new Product(){ProductName="Samsung S9",Price=6000,ImageUrl="1.jpg",Description="<p>Bu bir açýklamadýr.</p>",CategoryId=eCommerceDbContext.Categories.FirstOrDefault().CategoryId},
                        });
                        eCommerceDbContext.SaveChanges();
                    }
                    if (!appIdentityDbContext.Roles.Any())
                    {
                        IdentityRole r1 = new IdentityRole() { Name = "SysAdmin" };
                        IdentityRole r2 = new IdentityRole() { Name = "Admin" };
                        IdentityRole r3 = new IdentityRole() { Name = "Customer" };
                        roleManager.CreateAsync(r1).Wait();
                        roleManager.CreateAsync(r2).Wait();
                        roleManager.CreateAsync(r3).Wait();
                        appIdentityDbContext.SaveChanges();

                        IdentityUser u1 = new IdentityUser { UserName = "ahmet", Email = "ahmet@gmail.com" };
                        IdentityUser u2 = new IdentityUser { UserName = "mehmet", Email = "mehmet@gmail.com" };
                        IdentityUser u3 = new IdentityUser { UserName = "hasan", Email = "hasan@gmail.com" };
                        userManager.CreateAsync(u1, "Ahmet3434*").Wait();
                        userManager.CreateAsync(u2, "Mehmet3434*").Wait();
                        userManager.CreateAsync(u3, "Hasan3434*").Wait();
                        appIdentityDbContext.SaveChanges();
                        eCommerceDbContext.SaveChanges();
                        userManager.AddToRoleAsync(u1, "SysAdmin").Wait();
                        userManager.AddToRoleAsync(u2, "Admin").Wait();
                        userManager.AddToRoleAsync(u3, "Customer").Wait();
                        appIdentityDbContext.SaveChanges();
                    }
                    host.Run();
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
