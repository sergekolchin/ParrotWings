using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PW.WebAPI.ViewModels
{
    public class UserTransactionViewModel
    {
        public int Id { get; set; }
        public string UserFromId { get; set; }
        public string UserFromName { get; set; }
        public string UserToId { get; set; }
        public string UserToName { get; set; }
        public int Amount { get; set; }
        public DateTime CreationDate { get; set; }
        public int CurrentBalance { get; set; }
    }
}