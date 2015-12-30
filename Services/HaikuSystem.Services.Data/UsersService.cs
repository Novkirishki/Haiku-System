namespace HaikuSystem.Services.Data
{
    using System;
    using HaikuSystem.Services.Data.Contracts;
    using HaikuSystem.Data.Models;
    using HaikuSystem.Data.Repositories;

    public class UsersService : IUsersService
    {
        private IRepository<User> users;

        public UsersService(IRepository<User> users)
        {
            this.users = users;
        }

        public void Register(string username, string publishCode)
        {
            var newUser = new User
            {
                Username = username,
                PublishCode = publishCode
            };

            this.users.Add(newUser);
            this.users.SaveChanges();
        }
    }
}
