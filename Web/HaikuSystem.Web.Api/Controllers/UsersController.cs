namespace HaikuSystem.Web.Api.Controllers
{
    using AutoMapper;
    using AutoMapper.QueryableExtensions;
    using Infrastructure.Validation;
    using Models;
    using Services.Data.Contracts;
    using System.Data.Entity.Infrastructure;
    using System.Linq;
    using System.Web.Http;

    [RoutePrefix("api/users")]
    public class UsersController : ApiController
    {
        private IUsersService users;

        public UsersController(IUsersService users)
        {
            this.users = users;
        }

        [HttpPost]
        [ValidateModel]
        [Route("login")]
        public IHttpActionResult Authenticate(UserRequestModel userModel)
        {
            var userFromDb = this.users.GetByUsername(userModel.Username).FirstOrDefault();

            if (userFromDb == null)
            {
                return Ok(new { success = false, message = "Such user does not exist" });
            }

            if (!(userModel.Username == userFromDb.Username && userModel.PublishCode == userFromDb.PublishCode))
            {
                return Ok(new { success = false, message = "Username and publish code does not match" });
            }

            return Ok(new { success = true });
        }

        [HttpPost]
        [ValidateModel]
        public IHttpActionResult Register([FromBody] UserRequestModel user)
        {
            try
            {
                this.users.Register(user.Username, user.PublishCode);
            }
            catch (DbUpdateException)
            {
                return this.BadRequest("User with that username already exists!");
            }

            return this.Created("api/Users", string.Empty);
        }        

        [HttpGet]
        public IHttpActionResult Get([FromUri]int skip = 0, int take = 10, string sortBy = "rating", string sortType = "descending")
        {
            var result = this.users.GetAll(sortBy, sortType, skip, take).ProjectTo<UserResponseModel>().ToList();

            return this.Ok(result);
        }

        [HttpGet]
        [Route("{username}")]
        public IHttpActionResult GetByUsername([FromUri] string username)
        {
            var result = this.users.GetByUsername(username).ProjectTo<UserResponseModel>().FirstOrDefault();

            return this.Ok(Mapper.Map<UserResponseModel>(result));
        }
    }
}
