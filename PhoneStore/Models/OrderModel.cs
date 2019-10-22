using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PhoneStore.Models
{
    public enum Status
    {
        Active,
        NonActive
    }

    public class OrderModel
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public UserModel User { get; set; }
        public DateTime? Date { get; set; }
        public Status Status { get; set; }
        public string Address { get; set; }
        public List<PhoneModel> Phones { get; set; }
    }
}
