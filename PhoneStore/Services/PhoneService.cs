using Microsoft.EntityFrameworkCore;
using PhoneStore.Data;
using PhoneStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PhoneStore.Services
{
    public class PhoneService : IPhoneService
    {
        private readonly PhoneStoreDbContext context;

        public PhoneService(PhoneStoreDbContext context)
        {
            this.context = context;
        }

        public async Task<IEnumerable<PhoneModel>> GetAllItemsAsync()
        {
            var phones = await context.Phones
                .ToListAsync();
            return phones;
        }

        //public async Task<bool> AddPhoneAsync(PhoneModel newPhone)
        //{
        //    newPhone.ID = Guid.NewGuid();
        //    context.Phones.Add(newPhone);

        //    var saveResult = await context.SaveChangesAsync();
        //    return saveResult == 1;
        //}        

        //public async Task<bool> DeletePhoneAsync(Guid id)
        //{
        //    var product = await context.Phones
        //        .Where(x => x.ID == id)
        //        .AsNoTracking()
        //        .SingleOrDefaultAsync();

        //    context.Phones.Remove(product);
        //    var saveResult = await context.SaveChangesAsync();

        //    return saveResult == 1;
        //}        
    }
}
