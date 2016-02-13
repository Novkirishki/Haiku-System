namespace HaikuSystem.Web.Api.Controllers
{
    using AutoMapper.QueryableExtensions;
    using HaikuSystem.Services.Data.Contracts;
    using Models;
    using System.Linq;
    using System.Web.Http;
    using System.Web.Http.Cors;

    [RoutePrefix("api/admins")]
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class AdminsController : ApiController
    {
        private IHaikusService haikus;
        private IUsersService users;
        private IAbusementsService abusements;

        public AdminsController(IHaikusService haikus, IUsersService users, IAbusementsService abusements)
        {
            this.haikus = haikus;
            this.users = users;
            this.abusements = abusements;
        }

        [HttpGet]
        [Route("abusements")]
        public IHttpActionResult Get(int skip = 0, int take = 10)
        {
            if (!Request.Headers.Contains("PublishCode"))
            {
                return this.Unauthorized();
            }

            var publishCode = Request.Headers.GetValues("PublishCode").FirstOrDefault();

            if (!this.users.IsAdmin(publishCode))
            {
                return this.Unauthorized();
            }

            var result = this.abusements.GetAll(skip, take).ProjectTo<AbusementResponseModel>().ToList();

            return this.Ok(result);
        }

        [HttpDelete]
        [Route("delete/{username}")]
        public IHttpActionResult DeleteUser([FromUri] string username)
        {
            if (!Request.Headers.Contains("PublishCode"))
            {
                return this.Unauthorized();
            }

            var publishCode = Request.Headers.GetValues("PublishCode").FirstOrDefault();

            if (!this.users.IsAdmin(publishCode))
            {
                return this.Unauthorized();
            }

            this.users.DeleteByUsername(username);

            return this.Ok();
        }

        [HttpPut]
        [Route("users/promote/{username}")]
        public IHttpActionResult MakeUserVIP([FromUri] string username)
        {
            if (!Request.Headers.Contains("PublishCode"))
            {
                return this.Unauthorized();
            }

            var publishCode = Request.Headers.GetValues("PublishCode").FirstOrDefault();
            var user = this.users.GetByUsername(username).FirstOrDefault();

            if (user == null)
            {
                return this.BadRequest("Invalid username");
            }

            if (!this.users.IsAdmin(publishCode))
            {
                return this.Unauthorized();
            }

            this.users.MakeVIP(user.Id);

            return this.Ok();
        }
    }
}
