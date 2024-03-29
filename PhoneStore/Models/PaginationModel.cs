﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PhoneStore.Models
{
    public class PaginationModel
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public int TotalItems { get; set; }
        public int TotalPages
        {
            get { 
                return (int)Math.Ceiling((decimal)TotalItems / PageSize); 
            }
        }
    }
}
