using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PhoneStore.Models.Display
{
    public class GetOrderDetailsDisplay
    {
        public string Email { get; set; }

        public double TotalSum { get; set; }

        public List<PhoneDisplay> Phones { get; set; } = new List<PhoneDisplay>();
    }
}
