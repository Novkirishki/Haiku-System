namespace HaikuSystem.Web.Api.Controllers
{
    using AutoMapper;
    using AutoMapper.QueryableExtensions;
    using Infrastructure.Validation;
    using Models;
    using Services.Data.Contracts;
    using System;
    using System.Linq;
    using System.Web.Http;

    public class HaikusController : ApiController
    {
        private IHaikusService haikus;
        private IUsersService users;

        public HaikusController(IHaikusService haikus, IUsersService users)
        {
            this.haikus = haikus;
            this.users = users;
        }

        [HttpGet]
        public IHttpActionResult Get([FromUri]int skip = 0, int take = 10, string sortBy = "rating", string sortType = "descending")
        {
            var result = this.haikus.GetAll(sortBy, sortType, skip, take).ProjectTo<HaikuResponseModel>().ToList();

            return this.Ok(result);
        }

        [HttpPost]
        [ValidateModel]
        [Route("api/haikus/{id}/ratings")]
        public IHttpActionResult RateHaiku([FromUri] int id, [FromBody] RatingRequestModel rating)
        {
            try
            {
                this.haikus.Rate(id, rating.Rating);
            }
            catch (ArgumentException ex)
            {
                return this.BadRequest(ex.Message);
            }

            return this.Created("api/Haikus", string.Empty);
        }

        [HttpPost]
        [ValidateModel]
        [Route("api/users/{username}/haikus")]
        public IHttpActionResult AddHaiku([FromBody] HaikuRequestModel haiku, [FromUri] string username)
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

            var result = this.haikus.AddHaiku(haiku.Text, user.Id);

            return this.Created("api/Users", Mapper.Map<HaikuResponseCreatedModel>(result));
        }

        [HttpDelete]
        [Route("api/users/{username}/haikus/{haikuId}")]
        public IHttpActionResult DeleteHaiku([FromUri] string username, [FromUri] int haikuId)
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

            var haiku = this.haikus.GetById(haikuId);

            if (haiku == null)
            {
                return this.BadRequest("Invalid haiku Id");
            }

            if (haiku.UserId != user.Id)
            {
                return this.Unauthorized();
            }

            this.haikus.Delete(haiku);

            return this.Ok();
        }

        [HttpDelete]
        [Route("api/users/{username}/haikus/deleteall")]
        public IHttpActionResult DeleteAllHaikus([FromUri] string username)
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
            
            this.haikus.DeleteAll(user);

            return this.Ok();
        }

        [HttpPut]
        [ValidateModel]
        [Route("api/users/{username}/haikus/{haikuId}")]
        public IHttpActionResult UpdateHaiku([FromBody] HaikuRequestModel haikuModel, [FromUri] string username, [FromUri] int haikuId)
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

            var haiku = this.haikus.GetById(haikuId);

            if (haiku == null)
            {
                return this.BadRequest("Invalid haiku Id");
            }

            if (haiku.UserId != user.Id)
            {
                return this.Unauthorized();
            }

            haiku.Text = haikuModel.Text;
            this.haikus.UpdateHaiku(haiku);

            return this.Ok();
        }

        [HttpPost]
        [ValidateModel]
        [Route("api/haikus/{id}/abusements")]
        public IHttpActionResult AbuseHaiku([FromUri] int id, [FromBody] AbusementRequestModel abusement)
        {
            try
            {
                this.haikus.Abuse(id, abusement.Text);
            }
            catch (ArgumentException ex)
            {
                return this.BadRequest(ex.Message);
            }

            return this.Created("api/Haikus", string.Empty);
        }
    }
}
