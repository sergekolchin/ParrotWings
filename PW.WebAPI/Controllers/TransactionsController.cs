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

namespace PW.WebAPI.Controllers
{
    public class TransactionsController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/Transactions
        public IQueryable<UserTransaction> GetUserTransactions()
        {
            //get current user id
            var userId = User.Identity.GetUserId();
            return db.UserTransactions
                .Where(x => (Equals(x.UserFrom, userId) || Equals(x.UserTo, userId)));
        }

        // GET: api/Transactions/5
        [ResponseType(typeof(UserTransaction))]
        public async Task<IHttpActionResult> GetUserTransaction(int id)
        {
            UserTransaction userTransaction = await db.UserTransactions.FindAsync(id);
            if (userTransaction == null)
            {
                return NotFound();
            }

            return Ok(userTransaction);
        }

        // PUT: api/Transactions/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutUserTransaction(int id, UserTransaction userTransaction)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != userTransaction.Id)
            {
                return BadRequest();
            }

            db.Entry(userTransaction).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserTransactionExists(id))
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
        [ResponseType(typeof(UserTransaction))]
        public async Task<IHttpActionResult> PostUserTransaction(UserTransaction userTransaction)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.UserTransactions.Add(userTransaction);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = userTransaction.Id }, userTransaction);
        }

        // DELETE: api/Transactions/5
        [ResponseType(typeof(UserTransaction))]
        public async Task<IHttpActionResult> DeleteUserTransaction(int id)
        {
            UserTransaction userTransaction = await db.UserTransactions.FindAsync(id);
            if (userTransaction == null)
            {
                return NotFound();
            }

            db.UserTransactions.Remove(userTransaction);
            await db.SaveChangesAsync();

            return Ok(userTransaction);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool UserTransactionExists(int id)
        {
            return db.UserTransactions.Count(e => e.Id == id) > 0;
        }
    }
}