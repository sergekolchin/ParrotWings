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
            AutomaticMigrationsEnabled = true;
            ContextKey = "PW.WebAPI.Data.ApplicationDbContext";

            //uncomment for debug Seed()
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
            var user = new ApplicationUser();

            user = userManager.FindByEmail("user1@test.com");
            if (user == null)
            {
                userManager.Create(new ApplicationUser
                {
                    Name = "Scott Watson",
                    Email = "user1@test.com",
                    UserName = "user1@test.com"
                }, "Passw0rd!");
            }

            user = userManager.FindByEmail("user2@test.com");
            if (user == null)
            {
                userManager.Create(new ApplicationUser
                {
                    Name = "Robert Phillips",
                    Email = "user2@test.com",
                    UserName = "user2@test.com"
                }, "Passw0rd!");
            }

            user = userManager.FindByEmail("user3@test.com");
            if (user == null)
            {
                userManager.Create(new ApplicationUser
                {
                    Name = "Kelly Matthews",
                    Email = "user3@test.com",
                    UserName = "user3@test.com"
                }, "Passw0rd!");
            }

            //var users = context.Users
            //    .Where(x => (Equals(x.Email, "user1@test.com") || Equals(x.Email, "user2@test.com") || Equals(x.Email, "user3@test.com")))
            //    .ToArray();

            //var userCount = users.Count();
            //var transactions = new List<UserTransaction>();
            //var rnd = new Random();

            //for (int i = 0; i < 30; i++)
            //{
            //    //two unique random indexes
            //    var rndIndexes = Enumerable.Range(0, userCount + 1).OrderBy(g => Guid.NewGuid()).Take(2).ToArray();
            //    transactions.Add(new UserTransaction(users[rndIndexes[0]], users[rndIndexes[1]], rnd.Next(5,51)));
            //}
            //transactions.Add(new UserTransaction(users[1], users[0], 50));
            //transactions.Add(new UserTransaction(users[2], users[0], 50));
            //transactions.Add(new UserTransaction(users[0], users[1], 20));

            //context.UserTransactions.AddRange(transactions);
            //context.SaveChanges();
        }
    }
}
