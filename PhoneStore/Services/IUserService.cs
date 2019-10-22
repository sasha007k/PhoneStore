using Microsoft.AspNetCore.Mvc;
using PhoneStore.Models.Display;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PhoneStore.Services
{
    public interface IUserService
    {
        Task<ShoppingCartDisplay> GetShoppingCartAsync();

        Task<bool> CancelPhoneAsync(int id);

        Task<bool> CreateOrderAsync(string address);

        Task<List<GetOrderDisplay>> GetHistoryAsync();
    }
}
