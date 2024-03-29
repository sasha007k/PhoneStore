﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using PhoneStore.Data;
using PhoneStore.Models;
using PhoneStore.Models.Display;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PhoneStore.Services
{
    public class AdminService : IAdminService
    {
        private readonly PhoneStoreDbContext context;
        private readonly UserManager<UserModel> manager;
        private readonly IHttpContextAccessor httpContextAccessor;

        public AdminService(PhoneStoreDbContext context, UserManager<UserModel> manager, IHttpContextAccessor httpContextAccessor)
        {
            this.context = context;
            this.manager = manager;
            this.httpContextAccessor = httpContextAccessor;
        }
        public List<GetOrderDisplay> GetOrdersAsync()
        {
            var orders = (from o in context.Orders
                          orderby o.Status
                          select new GetOrderDisplay(o.Id, o.Date, o.Address, o.Status)).
                         ToList();

            return orders;
        }
        public async Task<bool> OpenOrderAsync(int id)
        {
            var order = await context.Orders.FindAsync(id);

            order.Status = Status.Active;

            var saveResult = await context.SaveChangesAsync();

            return saveResult == 1;
        }

        public async Task<bool> CloseOrderAsync(int id)
        {
            var order = await context.Orders.FindAsync(id);

            order.Status = Status.NonActive;

            var saveResult = await context.SaveChangesAsync();

            return saveResult == 1;
        }

        public async Task<GetOrderDetailsDisplay> GetOrderDetailsAsync(int id)
        {
            var order = await context.Orders.FindAsync(id);

            var user = await manager.FindByIdAsync(order.UserId);

            var orderDetailsDisplay = new GetOrderDetailsDisplay()
            {
                Email = user.Email
            };

            var phones = (from i in context.Phones
                         where i.OrderId == order.Id
                         select new PhoneDisplay()
                         {
                             Id = i.Id,
                             Brand = i.Brand,
                             Model = i.Model,
                             Price = (i.Price - (i.Price * i.Sale / 100)),
                             Sale = i.Sale
                         }).ToList();

            double totalsum = phones.Sum(b => b.Price);

            orderDetailsDisplay.Phones = phones;
            orderDetailsDisplay.TotalSum = totalsum;

            return orderDetailsDisplay;
        }
    }
}
