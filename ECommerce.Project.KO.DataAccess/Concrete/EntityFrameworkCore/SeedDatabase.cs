using ECommerce.Project.KO.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Project.KO.DataAccess.Concrete.EntityFrameworkCore
{
    public static class SeedDatabase
    {
        public static void Seed()
        {
            var context = new ECommerceDbContext();
            //context üzerinden database özelliğine geçip database'e uygulanmamış migrationların olup olmadığını bu şekilde sorgulayıp işlme yaptırıyoruz.
            if (context.Database.GetPendingMigrations().Count() == 0)
            {
                if (context.Categories.Count() == 0)
                {
                    context.Categories.AddRange(Categories);
                    context.SaveChanges();
                    foreach (var item in Products)
                    {
                        item.CategoryId = context.Categories.FirstOrDefault().CategoryId;
                    }
                    context.Products.AddRange(Products);
                    context.SaveChanges();
                }
                

            }
        }

        private static Category[] Categories =
        {
            new Category() {CategoryName="Telefon"},
            new Category() {CategoryName="Bilgisayar"},
            new Category() {CategoryName="Elektronik"}
        };

        private static Product[] Products =
        {
            new Product() {ProductName="Samsung S5",Price=2000,ImageUrl="1.jpg",Description="<p>güzel telefon</p>"},
            new Product() {ProductName="Samsung S6",Price=3000,ImageUrl="1.jpg",Description="<p>güzel telefon</p>"},
            new Product() {ProductName="Samsung S7",Price=4000,ImageUrl="1.jpg",Description="<p>güzel telefon</p>"},
            new Product() {ProductName="Iphone 7",Price=4000,ImageUrl="1.jpg",Description="<p>güzel telefon</p>"},
            new Product() {ProductName="Iphone 8",Price=5000,ImageUrl="1.jpg",Description="<p>güzel telefon</p>"},

        };
    }
}
