using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using System;
using PW.WebAPI.Exceptions;
using System.ComponentModel.DataAnnotations;

namespace PW.WebAPI.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        [Required]
        public string Name { get; set; }
        public int Balance { get; private set; }

        public void Credit(int value)
        {
            if (value < 0) throw new ArgumentException(nameof(value));
            Balance += value;
        }

        public void Debit(int value)
        {
            if (value < 0) throw new ArgumentException(nameof(value));
            if (Balance - value < 0) throw new InvalidBalanceException();
            Balance -= value;
        }

        public ApplicationUser()
        {
            Balance = 500;
        }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager, string authenticationType)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, authenticationType);
            // Add custom user claims here
            return userIdentity;
        }
    }
}