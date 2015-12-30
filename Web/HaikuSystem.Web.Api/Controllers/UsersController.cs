namespace HaikuSystem.Web.Api.Controllers
{
    using Infrastructure.Validation;
    using Models;
    using Services.Data.Contracts;
    using System;
    using System.Web.Http;

    public class UsersController : ApiController
    {
        private IUsersService users;

        public UsersController(IUsersService users)
        {
            this.users = users;
        }

        [HttpPost]
        [ValidateModel]
        public IHttpActionResult Register([FromBody] UserRequestModel user)
        {
            try
            {
                this.users.Register(user.Username, user.PublishCode);
            }
            catch (Exception ex)
            {
                return this.BadRequest(ex.Message);
            }

            return this.Created("api/Users", string.Empty);
        }
    }
}
