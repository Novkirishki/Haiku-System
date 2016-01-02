namespace HaikuSystem.Web.Api.Controllers
{
    using AutoMapper.QueryableExtensions;
    using HaikuSystem.Services.Data.Contracts;
    using Models;
    using System.Linq;
    using System.Web.Http;

    [RoutePrefix("api/admins")]
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

        [HttpDelete]
        [Route("{username}/haikus/{id}")]
        public IHttpActionResult DeleteHaikuAdmin([FromUri]string username, int id)
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

            if (!(user.PublishCode == publishCode))
            {
                return this.Unauthorized();
            }

            if (!user.IsAdmin)
            {
                return this.Unauthorized();
            }

            this.haikus.DeleteById(id);

            return this.Ok();
        }

        [HttpGet]
        [Route("{username}/abusements")]
        public IHttpActionResult Get([FromUri]string username, int skip = 0, int take = 10)
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

            if (!(user.PublishCode == publishCode))
            {
                return this.Unauthorized();
            }

            if (!user.IsAdmin)
            {
                return this.Unauthorized();
            }

            var result = this.abusements.GetAll(skip, take).ProjectTo<AbusementResponseModel>().ToList();

            return this.Ok(result);
        }

        [HttpDelete]
        [Route("{username}/users/{id}")]
        public IHttpActionResult DeleteUser([FromUri] string username, int id)
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

            if (!(user.PublishCode == publishCode))
            {
                return this.Unauthorized();
            }

            if (!user.IsAdmin)
            {
                return this.Unauthorized();
            }

            this.users.DeleteById(id);

            return this.Ok();
        }

        [HttpPut]
        [Route("{username}/users/{id}")]
        public IHttpActionResult MakeUserVIP([FromUri] string username, int id)
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

            if (!(user.PublishCode == publishCode))
            {
                return this.Unauthorized();
            }

            if (!user.IsAdmin)
            {
                return this.Unauthorized();
            }

            this.users.MakeVIP(id);

            return this.Ok();
        }
    }
}
