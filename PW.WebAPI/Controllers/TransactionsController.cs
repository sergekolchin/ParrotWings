using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using PW.WebAPI.Models;
using Microsoft.AspNet.Identity;
using PW.WebAPI.Data;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using PW.WebAPI.ViewModels;
using System.Web.Http.Cors;
using PW.WebAPI.Infrastructure;

namespace PW.WebAPI.Controllers
{
    [Authorize]
    [RoutePrefix("api/Transactions")]
    public class TransactionsController : ApiController
    {
        private ApplicationDbContext ctx = new ApplicationDbContext();
        private IMapper _mapper = null;
        protected IMapper mapper
        {
            get
            {
                if (_mapper == null) _mapper = WebApiApplication.mapperConfiguration.CreateMapper();
                return _mapper;
            }
        }

        // GET: api/Transactions
        // Get all transactions that relate to the current user (From, To)
        public IQueryable<TransactionViewModel> GetTransactions(string id)
        {
            //var userId = User.Identity.GetUserId() //error - return old userId
            return ctx.Transactions
                    .Where(x => (x.UserFrom.Id.Equals(id) || x.UserTo.Id.Equals(id)))
                    .Include(x => x.UserFrom).Include(x => x.UserTo)
                    .OrderByDescending(x => x.CreationDate).ThenByDescending(x => x.Id)
                    .ProjectTo<TransactionViewModel>(mapper.ConfigurationProvider);
        }

        //api/Transactions/ByUser
        //Get all transactions made by the current user (From)
        [Route("ByUser")]
        public IQueryable<TransactionViewModel> GetTransactionsByUser()
        {
            //get current user id
            var userId = User.Identity.GetUserId();
            return ctx.Transactions
                    .Where(x => x.UserFrom.Id.Equals(userId))
                    .Include(x => x.UserFrom).Include(x => x.UserTo)
                    .OrderByDescending(x => x.CreationDate).ThenByDescending(x => x.Id)
                    .ProjectTo<TransactionViewModel>(mapper.ConfigurationProvider);
        }

        // GET: api/Transactions/5
        //[ResponseType(typeof(Transaction))]
        //public async Task<IHttpActionResult> GetTransaction(int id)
        //{
        //    Transaction transaction = await ctx.Transactions.FindAsync(id);
        //    if (transaction == null)
        //    {
        //        return NotFound();
        //    }

        //    return Ok(transaction);
        //}

        // PUT: api/Transactions/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutTransaction(int id, Transaction transaction)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != transaction.Id)
            {
                return BadRequest();
            }

            ctx.Entry(transaction).State = EntityState.Modified;

            try
            {
                await ctx.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TransactionExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Transactions
        [ResponseType(typeof(TransactionViewModel))]
        public async Task<IHttpActionResult> PostTransaction(TransactionViewModel transaction)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var dbTransaction = new Transaction();
                var userFrom = await ctx.Users.FirstOrDefaultAsync(x => x.Id == transaction.UserFromId);
                userFrom.Debit(transaction.Amount);
                var userTo = await ctx.Users.FirstOrDefaultAsync(x => x.Id == transaction.UserToId);
                userTo.Credit(transaction.Amount);

                dbTransaction.UserFrom = userFrom;
                dbTransaction.UserTo = userTo;
                dbTransaction.CreationDate = DateTime.UtcNow;
                dbTransaction.UserFromBalance = userFrom.Balance;
                dbTransaction.UserToBalance = userTo.Balance;
                dbTransaction.Amount = transaction.Amount;

                ctx.Transactions.Add(dbTransaction);
                await ctx.SaveChangesAsync();

                transaction = mapper.Map<TransactionViewModel>(dbTransaction);

                return Ok(transaction);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // DELETE: api/Transactions/5
        [ResponseType(typeof(Transaction))]
        public async Task<IHttpActionResult> DeleteTransaction(int id)
        {
            Transaction transaction = await ctx.Transactions.FindAsync(id);
            if (transaction == null)
            {
                return NotFound();
            }

            ctx.Transactions.Remove(transaction);
            await ctx.SaveChangesAsync();

            return Ok(transaction);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                ctx.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool TransactionExists(int id)
        {
            return ctx.Transactions.Any(e => e.Id == id);
        }
    }
}