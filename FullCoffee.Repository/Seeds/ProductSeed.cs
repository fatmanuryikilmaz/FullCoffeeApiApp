using FullCoffee.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FullCoffee.Repository.Seeds
{
    internal class ProductSeed : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasData(
                new Product
               {Id=1,CategoryId=1,Name="Kenya",Price="200₺", CreatedDate=DateTime.Now },
                new Product

                { Id =2,CategoryId=2,Name="Dark Chocolate",Price="100₺",CreatedDate=DateTime.Now },
                new Product

                { Id =3,CategoryId=3,Name="Turkish Coffee",Price="45.99₺",CreatedDate=DateTime.Now }

                );
        }
    }
}
