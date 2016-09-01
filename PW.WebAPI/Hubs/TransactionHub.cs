using System;
using System.Linq;
using System.Data.Entity;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.SignalR;
using PW.WebAPI.Data;
using System.Threading.Tasks;
using System.Collections.Generic;
using PW.WebAPI.Models;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using PW.WebAPI.ViewModels;


namespace PW.WebAPI.Hubs
{
    [Authorize]
    public class TransactionHub : Hub
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public void GreetAll()
        {
            Clients.All.acceptGreet($"All hail! Server time is { DateTime.Now.ToString() }");
        }

        // Register new user
        public override Task OnConnected()
        {
            string userId = Context.User.Identity.GetUserId();
          
            var transactions = new List<UserTransactionViewModel>();

            //add to group with "userId" name
            Groups.Add(Context.ConnectionId, userId);

            using (var ctx = new ApplicationDbContext())
            {
                Mapper.Initialize(cfg => cfg.CreateMap<UserTransaction, UserTransactionViewModel>());
                transactions = ctx.UserTransactions
                    .Where(x => (x.UserFrom.Id.Equals(userId) || x.UserTo.Id.Equals(userId)))
                    .Include(x => x.UserFrom).Include(x => x.UserTo)
                    .OrderByDescending(x => x.CreationDate)
                    .ProjectTo<UserTransactionViewModel>()
                    .ToList();
            }

            //send all transactions
            Clients.Group(userId).onConnected(transactions);

            return base.OnConnected();
        }
    }
}