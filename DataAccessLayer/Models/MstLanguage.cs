using System;
using System.Collections.Generic;

namespace DataAccessLayer.Models
{
    public partial class MstLanguage
    {
        public MstLanguage()
        {
            MstUser = new HashSet<MstUser>();
        }

        public int LanguageId { get; set; }
        public string LanguageName { get; set; }

        public virtual ICollection<MstUser> MstUser { get; set; }
    }
}
