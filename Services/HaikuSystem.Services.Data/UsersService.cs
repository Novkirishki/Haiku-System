namespace HaikuSystem.Services.Data
{
    using HaikuSystem.Services.Data.Contracts;
    using HaikuSystem.Data.Models;
    using HaikuSystem.Data.Repositories;
    using System.Linq;
    using System;

    public class UsersService : IUsersService
    {
        private IRepository<User> users;

        public UsersService(IRepository<User> users)
        {
            this.users = users;
        }

        public IQueryable<User> GetAll(string sortBy, string sortType, int skip, int take)
        {
            var result = this.users.All();

            switch (sortBy)
            {
                case "username":
                    switch (sortType)
                    {
                        case "ascending":
                            result = result.OrderBy(u => u.Username);
                            break;
                        case "descending":
                            result = result.OrderByDescending(u => u.Username);
                            break;
                    }
                    break;
                case "rating":
                    switch (sortType)
                    {
                        case "ascending":
                            result = result.OrderBy(u => u.Haikus.Count == 0 ? 0 : ((double)u.Haikus.Sum(h => h.Ratings.Count == 0 ? 0 : h.Ratings.Sum(r => r.Value)) / (u.Haikus.Sum(h => h.Ratings.Count) == 0 ? 1 : u.Haikus.Sum(h => h.Ratings.Count))));
                            break;
                        case "descending":
                            result = result.OrderBy(u => u.Haikus.Count == 0 ? 0 : ((double)u.Haikus.Sum(h => h.Ratings.Count == 0 ? 0 : h.Ratings.Sum(r => r.Value)) / (u.Haikus.Sum(h => h.Ratings.Count) == 0 ? 1 : u.Haikus.Sum(h => h.Ratings.Count))));
                            break;
                    }
                    break;
            }

            return result.Skip(skip).Take(take);
        }

        public IQueryable<User> GetByPublishCode(string publishCode)
        {
            return this.users.All().Where(u => u.PublishCode == publishCode);
        }

        public IQueryable<User> GetByUsername(string username)
        {
            return this.users.All().Where(u => u.Username == username);
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
