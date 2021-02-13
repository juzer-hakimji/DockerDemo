using System;
using System.Collections.Generic;
using System.Text;

namespace ViewModels
{
    public class UserVM
    {

        public UserVM()
        {
            CountryList = new List<CountryVM>();
        }

        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MobileNumber { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public int? CountryId { get; set; }
        public string Gender { get; set; }
        public string LanguageId { get; set; }
        public string Remarks { get; set; }
        public string Token { get; set; }
        public List<CountryVM> CountryList { get; set; }

    }
}
