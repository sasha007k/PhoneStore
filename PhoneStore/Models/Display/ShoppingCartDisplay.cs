using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PhoneStore.Models.Display
{
    public class ShoppingCartDisplay
    {
        public double TotalPrice { get; set; }

        public List<PhoneDisplay> Phones { get; set; }
    }
}
