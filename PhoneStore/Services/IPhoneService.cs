using PhoneStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PhoneStore.Services
{
    public interface IPhoneService
    {
        Task<IEnumerable<PhoneModel>> GetAllItemsAsync();

        Task<bool> AddPhoneAsync(PhoneModel newPhone);

        Task<bool> DeletePhoneAsync(int id);

        Task<bool> AddPhoneToShoppingCartAsync(int id);
    }
}
