using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PW.WebAPI.ViewModels
{
    public class TransactionViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "UserFrom is required")]
        public string UserFromId { get; set; }
        public string UserFromName { get; set; }

        [Required(ErrorMessage = "UserTo is required")]
        public string UserToId { get; set; }
        public string UserToName { get; set; }

        [Required(ErrorMessage = "Amount is required")]
        [Range(1, int.MaxValue, ErrorMessage = "Amount must be greater than zero")]
        public int Amount { get; set; }

        public DateTime CreationDate { get; set; }
        public int UserFromBalance { get; set; }
        public int UserToBalance { get; set; }
        public bool Credit { get; set; }
    }
}