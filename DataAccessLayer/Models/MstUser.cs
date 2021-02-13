using System;
using System.Collections.Generic;

namespace DataAccessLayer.Models
{
    public partial class MstUser
    {
        public MstUser()
        {
            MstCountry = new HashSet<MstCountry>();
        }

        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MobileNumber { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public int? CountryId { get; set; }
        public int? Gender { get; set; }
        public int? LanguageId { get; set; }
        public string Remarks { get; set; }

        public virtual MstCountry Country { get; set; }
        public virtual MstLanguage Language { get; set; }
        public virtual ICollection<MstCountry> MstCountry { get; set; }
    }
}
