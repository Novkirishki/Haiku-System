﻿namespace HaikuSystem.Services.Data
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

        public void DeleteByUsername(string username)
        {
            var user = this.GetByUsername(username).FirstOrDefault();
            this.users.Delete(user);
            this.users.SaveChanges();
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
                            result = result.OrderByDescending(u => u.IsVIP)
                                .ThenBy(u => u.Username)
                                .Skip(skip)
                                .Take(take);
                            break;
                        case "descending":
                            result = result
                                .OrderByDescending(u => u.IsVIP)
                                .ThenByDescending(u => u.Username)
                                .Skip(skip)
                                .Take(take);
                            break;
                    }
                    break;
                case "rating":
                    switch (sortType)
                    {
                        case "ascending":
                            result = result
                                .OrderByDescending(u => u.IsVIP)
                                .ThenBy(u => u.Haikus.Count == 0 ? 0 : ((double)u.Haikus.Sum(h => h.Ratings.Count == 0 ? 0 : h.Ratings.Sum(r => r.Value)) / (u.Haikus.Sum(h => h.Ratings.Count) == 0 ? 1 : u.Haikus.Sum(h => h.Ratings.Count))))
                                .Skip(skip)
                                .Take(take);
                            break;
                        case "descending":
                            result = result
                                .OrderByDescending(u => u.IsVIP)
                                .ThenByDescending(u => u.Haikus.Count == 0 ? 0 : ((double)u.Haikus.Sum(h => h.Ratings.Count == 0 ? 0 : h.Ratings.Sum(r => r.Value)) / (u.Haikus.Sum(h => h.Ratings.Count) == 0 ? 1 : u.Haikus.Sum(h => h.Ratings.Count))))
                                .Skip(skip)
                                .Take(take);
                            break;
                    }
                    break;
            }
            
            return result;
        }

        public IQueryable<User> GetByPublishCode(string publishCode)
        {
            return this.users.All().Where(u => u.PublishCode == publishCode);
        }

        public IQueryable<User> GetByUsername(string username)
        {
            return this.users.All().Where(u => u.Username == username);
        }

        public bool IsAdmin(string publishCode)
        {
            var user = this.users.All().Where(u => u.PublishCode == publishCode).FirstOrDefault();

            if (user != null)
            {
                return user.IsAdmin;
            }

            return false;
        }

        public void MakeVIP(int id)
        {
            var user = this.users.GetById(id);
            user.IsVIP = true;

            this.users.Update(user);
            this.users.SaveChanges();
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
