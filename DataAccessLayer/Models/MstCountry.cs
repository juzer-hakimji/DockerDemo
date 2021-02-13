using System;
using System.Collections.Generic;

namespace DataAccessLayer.Models
{
    public partial class MstCountry
    {
        public MstCountry()
        {
            MstUser = new HashSet<MstUser>();
        }

        public int CountryId { get; set; }
        public string CountryName { get; set; }
        public string CountryCode { get; set; }
        public int UserId { get; set; }
        public bool? IsActive { get; set; }

        public virtual MstUser User { get; set; }
        public virtual ICollection<MstUser> MstUser { get; set; }
    }
}
