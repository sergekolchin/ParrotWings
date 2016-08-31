using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;

namespace PW.WebAPI.Hubs
{
    public class TransactionHub : Hub
    {
        public void GreetAll()
        {
            Clients.All.acceptGreet($"Server: The time is { DateTime.Now.ToString() }");
        }
    }
}