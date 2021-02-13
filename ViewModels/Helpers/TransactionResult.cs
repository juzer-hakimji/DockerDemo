using System;
using System.Collections.Generic;
using System.Text;

namespace ViewModels.Helpers
{
    public class TransactionResult<T>
    {
        public bool Success { get; set; }
        public string RedirectURL { get; set; }
        public string Message { get; set; }
        public T Data { get; set; }
    }
}
