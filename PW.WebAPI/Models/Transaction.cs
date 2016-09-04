using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PW.WebAPI.Models
{
    public class Transaction
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public ApplicationUser UserFrom { get; set; }

        [Required]
        public ApplicationUser UserTo { get; set; }

        [Required]
        public int Amount { get; set; }

        public DateTime CreationDate { get; set; }
        public int UserFromBalance { get; set; }
        public int UserToBalance { get; set; }
    }
}