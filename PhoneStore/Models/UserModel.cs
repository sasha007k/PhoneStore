using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PhoneStore.Models
{
    public class UserModel : IdentityUser
    {
        public string City { get; set; }
        public string Street { get; set; }
        public ShoppingCart ShoppingCart { get; set; }
        public List<OrderModel> Orders { get; set; }
    }
}
