using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PhoneStore.Data;
using PhoneStore.Models;
using PhoneStore.Models.Display;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace PhoneStore.Services
{
    public class UserService : IUserService
    {
        private readonly PhoneStoreDbContext context;
        private readonly UserManager<UserModel> manager;
        private readonly IHttpContextAccessor httpContextAccessor;

        public UserService(PhoneStoreDbContext context, UserManager<UserModel> manager, IHttpContextAccessor httpContextAccessor)
        {
            this.context = context;
            this.manager = manager;
            this.httpContextAccessor = httpContextAccessor;
        }
        public async Task<ShoppingCartDisplay> GetShoppingCartAsync()
        {
            var userId = httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var request = await manager.FindByIdAsync(userId);
            var requestedUser = await manager.FindByNameAsync(request.UserName);

            var shoppingCart = (from i in context.ShoppingCarts
                                where i.UserId.Equals(requestedUser.Id)
                                select i).ToList().First();

            var phones = from i in context.Phones
                         where i.ShoppingCartId == shoppingCart.Id
                         select new PhoneDisplay() { Brand = i.Brand, Model = i.Model, Price = i.Price, Id = i.Id };

            double sum = phones.Sum(b => b.Price);

            var shoppingCartDisplay = new ShoppingCartDisplay() { TotalPrice = sum, Phones = phones.ToList() };

            return shoppingCartDisplay;
        }

        public async Task<bool> CancelPhoneAsync(int id)
        {
            var phone = await context.Phones.FindAsync(id);
            phone.ShoppingCartId = null;

            var saveResult = await context.SaveChangesAsync();
            return saveResult == 1;
        }

        public async Task<bool> CreateOrderAsync(string address)
        {
            var userId = httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var request = await manager.FindByIdAsync(userId);
            var requestedUser = await manager.FindByNameAsync(request.UserName);

            var order = new OrderModel()
            {
                Address = address,
                Date = DateTime.Now,
                Status = Status.Active,
                UserId = requestedUser.Id,
                User = requestedUser
            };


            var shoppingCart = (from i in context.ShoppingCarts
                             where i.UserId.Equals(requestedUser.Id)
                             select i).ToList<ShoppingCart>().First();

            var phones = (from i in context.Phones
                         where i.ShoppingCartId == shoppingCart.Id
                         select i).ToList<PhoneModel>();

            phones.ForEach(p =>
            {
                p.OrderId = order.Id;
                p.Order = order;
                p.ShoppingCartId = null;
                p.ShoppingCart = null;
            });

            var saveResult = await context.SaveChangesAsync();

            return saveResult == 4;
        }
    }
}
