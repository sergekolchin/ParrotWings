using PW.WebAPI.Data;
using PW.WebAPI.ViewModels;
using System.Linq;
using System.Web.Http;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using PW.WebAPI.Models;
using System.Web.Http.Description;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;

namespace PW.WebAPI.Controllers
{
    [Authorize]
    [RoutePrefix("api/Users")]
    public class UsersController : ApiController
    {
        private ApplicationDbContext ctx = new ApplicationDbContext();

        // GET: api/Users
        // Get all users exclude the current
        public IQueryable<UserViewModel> GetUsers()
        {
            //Mapper.Map<ApplicationUser, UserViewModel>();
            //get the current user id
            var userId = User.Identity.GetUserId();
            return ctx.Users.Where(x => x.Id != userId)
                .OrderBy(x => x.Name)
                .Select(s => new UserViewModel {
                    Id = s.Id,
                    Name = s.Name,
                    Balance = s.Balance
                });
                //.ProjectTo<UserViewModel>(config);
        }

        // GET: api/Users/5
        [ResponseType(typeof(Transaction))]
        public IHttpActionResult GetUser(int id)
        {
            //get the current user id
            var userId = User.Identity.GetUserId();
            var user = ctx.Users.Where(x => x.Id == userId)
                    .Select(s => new UserViewModel
                    {
                        Id = s.Id,
                        Name = s.Name,
                        Balance = s.Balance
                    }).FirstOrDefault();

            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }


        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                ctx.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}