namespace PW.WebAPI.Migrations
{
    using Data;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Models;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<PW.WebAPI.Data.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            ContextKey = "PW.WebAPI.Data.ApplicationDbContext";

            //uncomment for debug seed()
            //if (System.Diagnostics.Debugger.IsAttached == false)
            //{
            //    System.Diagnostics.Debugger.Launch();
            //}
        }

        protected override void Seed(Data.ApplicationDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //create users
            var userManager = new ApplicationUserManager(new UserStore<ApplicationUser>(context));
            userManager.Create(new ApplicationUser
            {
                Name = "User1",
                Email = "user1@test.com",
                UserName = "user1@test.com"
            }, "Passw0rd!");

            userManager.Create(new ApplicationUser
            {
                Name = "User2",
                Email = "user2@test.com",
                UserName = "user2@test.com"
            }, "Passw0rd!");

            userManager.Create(new ApplicationUser
            {
                Name = "User3",
                Email = "user3@test.com",
                UserName = "user3@test.com"
            }, "Passw0rd!");

            //var users = context.Users
            //    .Where(x => (Equals(x.Email, "user1@test.com") || Equals(x.Email, "user2@test.com") || Equals(x.Email, "user3@test.com")))
            //    .ToArray();

            //var transactions = new List<UserTransaction>();
            //transactions.Add(new UserTransaction(users[1], users[0], 50));
            //transactions.Add(new UserTransaction(users[2], users[0], 50));
            //transactions.Add(new UserTransaction(users[0], users[1], 20));

            //context.UserTransactions.AddRange(transactions);
            //context.SaveChanges();
        }
    }
}
