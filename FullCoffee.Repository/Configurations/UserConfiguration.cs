using FullCoffee.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FullCoffee.Repository.Configurations
{
    internal class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x=>x.Id).UseIdentityColumn();
            builder.Property(x=>x.FirstName).IsRequired();
            builder.Property(x=>x.LastName).IsRequired();
            builder.Property(x=>x.Email).IsRequired();
            builder.Property(x=>x.Password).IsRequired();
        }
    }
}
