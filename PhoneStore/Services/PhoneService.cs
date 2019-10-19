using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using Microsoft.Owin.Host.SystemWeb;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PhoneStore.Data;
using PhoneStore.Models;
using System.Security.Claims;

namespace PhoneStore.Services
{
    public class PhoneService : IPhoneService
    {
        private readonly PhoneStoreDbContext context;
        private readonly UserManager<UserModel> manager;
        private readonly IHttpContextAccessor httpContextAccessor;

        public PhoneService(PhoneStoreDbContext context, UserManager<UserModel> manager, IHttpContextAccessor httpContextAccessor)
        {
            this.context = context;
            this.manager = manager;
            this.httpContextAccessor = httpContextAccessor;
        }

        public async Task<IEnumerable<PhoneModel>> GetAllItemsAsync()
        {
            var phones = await context.Phones
                .ToListAsync();
            return phones;
        }

        public async Task<bool> AddPhoneAsync(PhoneModel newPhone)
        {
            context.Phones.Add(newPhone);

            var saveResult = await context.SaveChangesAsync();
            return saveResult == 1;
        }

        public async Task<bool> DeletePhoneAsync(int id)
        {
            var product = context.Phones.Find(id);

            context.Phones.Remove(product);
            var saveResult = await context.SaveChangesAsync();

            return saveResult == 1;
        }

        public async Task<bool> AddPhoneToShoppingCartAsync(int id)
        {
            var product = context.Phones.Find(id);
            var userId = httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;

            var request = await manager.FindByIdAsync(userId);

            var bucket = (from i in context.ShoppingCarts
                          where i.UserId.Equals(userId)
                          select i).ToList().FirstOrDefault();

            UserModel requestedUser = await manager.FindByNameAsync(request.UserName);

            product.ShoppingCart = requestedUser.ShoppingCart;
            product.ShoppingCart.Id = requestedUser.ShoppingCart.Id;
            bucket.Phones.Add(product);
            var saveResult = await context.SaveChangesAsync();

            return saveResult == 1;
        }
    }
}