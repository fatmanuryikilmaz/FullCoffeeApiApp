using FullCoffee.Core.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace FullCoffee.Repository
{
    public class AppDbContext : IdentityDbContext<User, IdentityRole<int>, int>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductDetail> ProductDetails { get; set; }
        public DbSet<Blog> Blogs { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            modelBuilder.Entity<ProductDetail>().HasData(
                new ProductDetail()
                { Id = 1, FavoryCount = 1071, ReviewCount = 30, Description = "coffee beans from Kenya ", ProductId = 1 },
                new ProductDetail()
                { Id = 2, FavoryCount = 1010, ReviewCount = 56, Description = "handmade dark chocolate with %70 cocoa", ProductId = 2 },
                new ProductDetail()
                { Id = 3, FavoryCount = 1453, ReviewCount = 300, Description = "freshly grounded Turkish Coffee", ProductId = 3 }
            );

            base.OnModelCreating(modelBuilder);
        }
    }
}
