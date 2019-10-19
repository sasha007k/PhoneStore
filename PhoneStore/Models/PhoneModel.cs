using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PhoneStore.Models
{
    public class PhoneModel
    {
        public int Id { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public int Year { get; set; }
        public double Price { get; set; }

        public int? ShoppingCartId { get; set; }
        public ShoppingCart ShoppingCart { get; set; }

        public int? OrderId { get; set; }
        public OrderModel Order { get; set; }
    }
}
