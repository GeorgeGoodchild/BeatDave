using System.Linq;
using System.Net.Http;
using System.Web.Security;
using BeatDave.Domain;
using BeatDave.Web.Areas.Api_v1.Models;
using BeatDave.Web.Infrastructure;

namespace BeatDave.Web.Areas.Api_v1.Controllers
{
    public class UsersController : FatApiController
    {
        // GET
        public HttpResponseMessage Get(string username)
        {
            var user = base.RavenSession.Query<User>()
                                        .SingleOrDefault(x => x.Id == username);


            // TODO: Limit the amount of data that goes back if it's not the user making the request
            var userView = user.MapTo<UserView>();

            return Ok(userView);
        }



        // POST /Api/v1/Users
        public HttpResponseMessage Post(UserInput userInput)
        {            
            if (ModelState.IsValid == false)
                return BadRequest(ModelState.FirstErrorMessage());

            var existingUser = base.RavenSession.Query<User>()
                                                .SingleOrDefault(x => x.Id == userInput.Username);

            if (existingUser != null)
                return BadRequest(string.Format("Username {0} is taken", userInput.Username));

            MembershipCreateStatus status;

            var membershipUser = Membership.CreateUser(userInput.Username,
                                                       userInput.Password,
                                                       userInput.Email,
                                                       null,
                                                       null,
                                                       true,
                                                       out status);

            if (status != MembershipCreateStatus.Success)
                return BadRequest(status.ToString());

            var user = new User() { AspNetId = membershipUser.ProviderUserKey.ToString() };
            userInput.MapToInstance(user);

            base.RavenSession.Store(user);

            var userView = user.MapTo<UserView>();

            return Created(userView);
        }
    }
}
