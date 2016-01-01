namespace HaikuSystem.Web.Api.Controllers
{
    using AutoMapper.QueryableExtensions;
    using Models;
    using Services.Data.Contracts;
    using System.Linq;
    using System.Web.Http;

    public class AbusementsController : ApiController
    {
        private IAbusementsService abusements;
        private IUsersService users;

        public AbusementsController(IAbusementsService abusements, IUsersService users)
        {
            this.abusements = abusements;
            this.users = users;
        }

        [HttpGet]
        public IHttpActionResult Get([FromUri]int skip = 0, int take = 10, string sortBy = "rating", string sortType = "descending")
        {
            if (!Request.Headers.Contains("PublishCode"))
            {
                return this.Unauthorized();
            }

            var publishCode = Request.Headers.GetValues("PublishCode").FirstOrDefault();
            var user = this.users.GetByPublishCode(publishCode).FirstOrDefault();

            if (user == null)
            {
                return this.BadRequest("Invalid code");
            }

            if (!user.IsAdmin)
            {
                return this.Unauthorized();
            }

            var result = this.abusements.GetAll(skip, take).ProjectTo<AbusementResponseModel>().ToList();

            return this.Ok(result);
        }
    }
}
