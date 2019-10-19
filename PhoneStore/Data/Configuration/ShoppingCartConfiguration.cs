using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PhoneStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PhoneStore.Data.Configuration
{
    public class ShoppingCartConfiguration : IEntityTypeConfiguration<ShoppingCart>
    {
        public void Configure(EntityTypeBuilder<ShoppingCart> builder)
        {
            builder.HasKey(b => b.Id);

            builder.ToTable("ShoppingCarts");

            builder.
                HasOne(b => b.User).
                WithOne(u => u.ShoppingCart).
                IsRequired();

            builder.
                HasMany(b => b.Phones).
                WithOne(b => b.ShoppingCart).
                HasForeignKey(b => b.ShoppingCartId);
        }
    }
}
