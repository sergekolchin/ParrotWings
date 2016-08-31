using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using PW.WebAPI.Models;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace PW.WebAPI.Data
{
    public class ApplicationDbContextInitializer : CreateDatabaseIfNotExists<ApplicationDbContext>
    {
        protected override async void Seed(ApplicationDbContext context)
        {
            //create some users
            var manager = new ApplicationUserManager(new UserStore<ApplicationUser>(context));
            var transactions = new List<UserTransaction>();

            if (!context.Users.Any(x => x.Email == "user1@test.com"))
            {
                await manager.CreateAsync(new ApplicationUser
                {
                    Name = "User1",
                    Email = "user1@test.com",
                    UserName = "user1@test.com"
                }, "Passw0rd!");
            }

            if (!context.Users.Any(x => x.Email == "user2@test.com"))
            {
                await manager.CreateAsync(new ApplicationUser
                {
                    Name = "User2",
                    Email = "user2@test.com",
                    UserName = "user2@test.com"
                }, "Passw0rd!");
            }

            if (!context.Users.Any(x => x.Email == "user3@test.com"))
            {
                await manager.CreateAsync(new ApplicationUser
                {
                    Name = "User3",
                    Email = "user3@test.com",
                    UserName = "user3@test.com"
                }, "Passw0rd!");
            }

            var users = context.Users
                .Where(x => (Equals(x.Email, "user1@test.com") || Equals(x.Email, "user2@test.com") || Equals(x.Email, "user3@test.com")))
                .ToArray();

            transactions.Add(new UserTransaction(users[1], users[0], 50));
            transactions.Add(new UserTransaction(users[2], users[0], 50));
            transactions.Add(new UserTransaction(users[0], users[1], 20));

            context.UserTransactions.AddRange(transactions);
            await context.SaveChangesAsync();

            base.Seed(context);
        }
    }
}