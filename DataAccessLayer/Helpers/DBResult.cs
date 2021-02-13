using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccessLayer.Helpers
{
    public class DBResult<U>
    {
        public U Data { get; set; }
        public bool TransactionResult { get; set; }
    }
}
