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
using PhoneStore.Models.Display;

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

        public GetPhonesDisplay GetAllItems(int page=1)
        {
            int pageSize = 4;

            var phones = (from i in context.Phones
                         where (i.ShoppingCartId == null) && (i.OrderId == null)
                         select new PhoneDisplay()
                         {
                             Id = i.Id,
                             Brand = i.Brand,
                             Model = i.Model,
                             Price = i.Price
                         }).Skip((page - 1) * pageSize).Take(pageSize).ToList(); ;

            var pageInfo = new PaginationModel { PageNumber = page, PageSize = pageSize, TotalItems = context.Phones.Count() };

            var phonesDisplay = new GetPhonesDisplay() { Phones = phones, PageInfo = pageInfo };

            return phonesDisplay;
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

            var shoppingCart = (from i in context.ShoppingCarts
                          where i.UserId.Equals(userId)
                          select i).ToList().FirstOrDefault();

            var requestedUser = await manager.FindByNameAsync(request.UserName);

            product.ShoppingCart = requestedUser.ShoppingCart;
            product.ShoppingCartId = requestedUser.ShoppingCart.Id;
            shoppingCart.Phones.Add(product);
            var saveResult = await context.SaveChangesAsync();

            return saveResult == 1;
        }
    }
}