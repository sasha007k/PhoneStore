using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PhoneStore.Models.Display
{
    public class GetPhonesDisplay
    {
        public List<PhoneDisplay> Phones { get; set; }

        public PaginationModel PageInfo { get; set; }
    }
}
