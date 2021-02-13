using System;
using System.Collections.Generic;
using System.Text;

namespace ViewModels
{
    public class CountryVM
    {
        public int? CountryId { get; set; }
        public string CountryName { get; set; }
        public string CountryCode { get; set; }
        public int UserId { get; set; }
    }
}
