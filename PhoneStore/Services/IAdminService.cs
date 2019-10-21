using PhoneStore.Models;
using PhoneStore.Models.Display;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PhoneStore.Services
{
    public interface IAdminService
    {
        List<GetOrderDisplay> GetOrdersAsync();

        Task<bool> OpenOrderAsync(int id);

        Task<bool> CloseOrderAsync(int id);
    }
}
