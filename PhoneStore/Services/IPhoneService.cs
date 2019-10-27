using PhoneStore.Models;
using PhoneStore.Models.Display;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PhoneStore.Services
{
    public interface IPhoneService
    {
        GetPhonesDisplay GetAllItems(int page=1);

        Task<bool> AddPhoneAsync(PhoneModel newPhone);

        Task<bool> DeletePhoneAsync(int id);

        Task<bool> AddSaleAsync(int id, double sale);

        Task<bool> AddPhoneToShoppingCartAsync(int id);
    }
}
