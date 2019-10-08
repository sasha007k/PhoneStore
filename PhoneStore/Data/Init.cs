using Microsoft.AspNetCore.Identity;
using PhoneStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PhoneStore.Data
{
    public static class Init
    {
        public static async Task SeedData(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager, PhoneStoreDbContext context)
        {
            await SeedRoles(roleManager);
            await SeedUsers(userManager, context);
        }
        public static async Task SeedUsers(UserManager<IdentityUser> userManager, PhoneStoreDbContext context)
        {
            string username = "admin@gmail.com";
            string password = "admin";
            if (await userManager.FindByNameAsync(username) == null)
            {
                UserModel admin = new UserModel() { UserName = username };
                IdentityResult result = await userManager.CreateAsync(admin, password);
                if (result.Succeeded)
                {
                    var shoppingCart = new ShoppingCart() { User = admin, UserId = admin.Id };
                    admin.ShoppingCart = shoppingCart;
                    await context.ShoppingCarts.AddAsync(shoppingCart);
                    await context.SaveChangesAsync();
                    await userManager.AddToRoleAsync(admin, "Admin");
                    await userManager.AddToRoleAsync(admin, "Shopper");
                }
            }
        }
        public static async Task SeedRoles(RoleManager<IdentityRole> roleManager)
        {
            string[] roleNames = { "Shopper", "Admin" };
            IdentityResult roleResult;
            foreach (var role in roleNames)
            {
                var roleExist = await roleManager.RoleExistsAsync(role);
                if (roleExist == false)
                {
                    roleResult = await roleManager.CreateAsync(new IdentityRole(role));
                }
            }
        }
    }
}
