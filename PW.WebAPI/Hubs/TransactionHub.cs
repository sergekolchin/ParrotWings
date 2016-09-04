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
using Newtonsoft.Json;
using System.Web.Http.ModelBinding;
using System.ComponentModel.DataAnnotations;

namespace PW.WebAPI.Hubs
{
    [Authorize]
    public class TransactionHub : Hub
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // Register new user
        public override Task OnConnected()
        {
            string userId = Context.User.Identity.GetUserId();
            var user = new UserViewModel();

            //add user to group with "userId" name
            Groups.Add(Context.ConnectionId, userId);

            using (var ctx = new ApplicationDbContext())
            {
                user = ctx.Users.Where(x => x.Id == userId)
                    .Select(s => new UserViewModel
                    {
                        Id = s.Id,
                        Name = s.Name,
                        Balance = s.Balance
                    }).FirstOrDefault();
            }

            //send current user info
            Clients.Group(userId).onConnected(user);

            return base.OnConnected();
        }

        public void addTransaction(TransactionViewModel transaction)
        {
            var userFrom = new UserViewModel();
            var userTo = new UserViewModel();
            
            //get the balance of the transaction users
            using (var ctx = new ApplicationDbContext())
            {
                userFrom = ctx.Users.Where(x => x.Id == transaction.UserFromId)
                    .Select(s => new UserViewModel
                    {
                        Id = s.Id,
                        Name = s.Name,
                        Balance = s.Balance
                    }).FirstOrDefault();

                userTo = ctx.Users.Where(x => x.Id == transaction.UserToId)
                    .Select(s => new UserViewModel
                    {
                        Id = s.Id,
                        Name = s.Name,
                        Balance = s.Balance
                    }).FirstOrDefault();
            }

            //notify all transaction users
            Clients.Group(transaction.UserFromId).transactionAdded(transaction, userFrom);
            Clients.Group(transaction.UserToId).transactionAdded(transaction, userTo);
        }
    }
}