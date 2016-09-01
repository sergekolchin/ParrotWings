using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PW.WebAPI.Models
{
    public class UserTransaction
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public ApplicationUser UserFrom { get; private set; } 

        [Required]
        public ApplicationUser UserTo { get; private set; }

        [Required]
        public int Amount { get; private set; }

        public DateTime CreationDate { get; private set; }
        public int CurrentBalance { get; private set; }

        public UserTransaction() { }

        public UserTransaction(ApplicationUser userFrom, ApplicationUser userTo, int amount )
        {
            try
            {
                UserFrom = userFrom;
                UserTo = userTo;
                Amount = amount;
                CreationDate = DateTime.UtcNow;
                userFrom.Debit(amount);
                userTo.Credit(amount);
                CurrentBalance = userFrom.Balance;
            }
            catch (Exception)
            {
                throw;
            }
            
        }
    }
}