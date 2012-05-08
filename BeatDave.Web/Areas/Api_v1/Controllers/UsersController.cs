using System.Linq;
using System.Net.Http;
using System.Web.Security;
using BeatDave.Web.Areas.Api_v1.Models;
using BeatDave.Web.Infrastructure;
using BeatDave.Web.Models;

namespace BeatDave.Web.Areas.Api_v1.Controllers
{
    public class UsersController : FatApiController
    {
        // POST /Api/v1/Users
        public HttpResponseMessage<UserView> Post(UserInput userInput)
        {
            if (ModelState.IsValid == false)
                return BadRequest<UserView>(null, ModelState.FirstErrorMessage());

            var existingUser = base.RavenSession.Query<User>()
                                                .SingleOrDefault(x => x.Username == userInput.Username);

            if (existingUser != null)
                return BadRequest<UserView>(null, string.Format("Username {0} is taken", userInput.Username));

            MembershipCreateStatus status;

            var membershipUser = Membership.CreateUser(userInput.Username,
                                                       userInput.Password,
                                                       userInput.Email,
                                                       null,
                                                       null,
                                                       true,
                                                       out status);

            if (status != MembershipCreateStatus.Success)
                return BadRequest<UserView>(null, status.ToString());

            var user = new User() { AspNetId = membershipUser.ProviderUserKey.ToString() };
            userInput.MapToInstance(user);

            base.RavenSession.Store(user);

            var userView = user.MapTo<UserView>();

            return Created(userView);
        }
    }
}
