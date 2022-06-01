using ECommerce.Project.KO.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Project.KO.DataAccess.Concrete.EntityFrameworkCore
{
    public class ECommerceDbContext : DbContext
    {


        //public ECommerceDbContext(DbContextOptions<ECommerceDbContext> options) : base(options)
        //{

        //}


        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Address> Addresses { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //@"User ID=sa;Password=Password12*;Host=localhost;Port=5440;Database=ECommercedb;Integrated Security=true;Pooling=true"
            optionsBuilder.UseSqlServer("Server=localhost,1433;Database=ECommercedb; User=sa; Password=Password12*", option =>
            {
                option.EnableRetryOnFailure();
            });

            base.OnConfiguring(optionsBuilder);

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
        }



    }
}
