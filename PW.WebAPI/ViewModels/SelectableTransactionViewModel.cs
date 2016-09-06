using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PW.WebAPI.ViewModels
{
    public class SelectableTransactionViewModel
    {
        public int Id { get; set; }
        public string UserToId { get; set; }
        public string UserToName { get; set; }
        public int Amount { get; set; }
        public string Detail
        {
            get
            {
                return UserToName + " PW" + Amount;
            }
        }
    }
}