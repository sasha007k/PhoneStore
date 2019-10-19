using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PhoneStore.Data.Configuration;
using PhoneStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PhoneStore.Data
{
    public class PhoneStoreDbContext : IdentityDbContext
    {        
        public DbSet<PhoneModel> Phones { get; set; }
        public DbSet<ShoppingCart> ShoppingCarts { get; set; }
        public DbSet<OrderModel> Orders { get; set; }

        public PhoneStoreDbContext(DbContextOptions<PhoneStoreDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ApplyConfiguration(new ShoppingCartConfiguration());
        }
    }
}
