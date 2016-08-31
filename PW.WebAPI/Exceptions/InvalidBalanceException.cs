using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PW.WebAPI.Exceptions
{
    public class InvalidBalanceException : ApplicationException
    {
        public InvalidBalanceException() : base("Balance should be greater than 0") { }
    }
}