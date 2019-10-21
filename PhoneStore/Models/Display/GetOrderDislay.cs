using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PhoneStore.Models.Display
{
    public class GetOrderDisplay
    {
        public int Id { get; set; }

        public DateTime? Date { get; set; }

        public string Address { get; set; }

        public Status Status { get; set; }

        public GetOrderDisplay(int id, DateTime? date, string address, Status status)
        {
            this.Id = id;
            this.Status = status;
            this.Date = date;
            this.Address = address;
        }
    }
}
