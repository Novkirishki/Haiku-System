﻿namespace HaikuSystem.Services.Data
{
    using HaikuSystem.Data.Models;
    using HaikuSystem.Data.Repositories;
    using HaikuSystem.Services.Data.Contracts;
    using System;
    using System.Linq;

    public class HaikusService : IHaikusService
    {
        private IHaikusService<Haiku> haikus;

        public HaikusService(IHaikusService<Haiku> haikus)
        {
            this.haikus = haikus;
        }

        public Haiku AddHaiku(string text, int userId)
        {
            var newHaiku = new Haiku
            {
                Text = text,
                UserId = userId,
                DatePublished = DateTime.Now
            };

            this.haikus.Add(newHaiku);
            this.haikus.SaveChanges();

            return newHaiku;
        }

        public void Delete(Haiku haiku)
        {
            this.haikus.Delete(haiku);
            this.haikus.SaveChanges();
        }

        public void DeleteAll(User user)
        {
            user.Haikus.ToList().ForEach(h => this.haikus.Delete(h));

            this.haikus.SaveChanges();
        }

        public IQueryable<Haiku> GetAll(string sortBy, string sortType, int skip, int take)
        {
            var result = this.haikus.All();

            switch (sortBy)
            {
                case "date":
                    switch (sortType)
                    {
                        case "ascending":
                            result = result.OrderBy(h => h.DatePublished);
                            break;
                        case "descending":
                            result = result.OrderByDescending(h => h.DatePublished);
                            break;
                    }
                    break;
                case "rating":
                    switch (sortType)
                    {
                        case "ascending":
                            result = result.OrderBy(h => h.Ratings.Count == 0 ? 0 : ((double)h.Ratings.Sum(r => r.Value) / h.Ratings.Count));
                            break;
                        case "descending":
                            result = result.OrderByDescending(h => h.Ratings.Count == 0 ? 0 : ((double)h.Ratings.Sum(r => r.Value) / h.Ratings.Count));
                            break;
                    }
                    break;
            }

            return result.Skip(skip).Take(take);
        }

        public Haiku GetById(int id)
        {
            return this.haikus.All().Where(h => h.Id == id).FirstOrDefault();
        }

        public void Rate(int haikuId, int rating)
        {
            var ratingToAdd = new Rating
            {
                Value = rating
            };

            var haiku = this.GetById(haikuId);

            if (haiku == null)
            {
                throw new ArgumentException("Invalid haiku Id");
            }

            haiku.Ratings.Add(ratingToAdd);
            this.haikus.Update(haiku);
            this.haikus.SaveChanges();
        }

        public void UpdateHaiku(Haiku haiku)
        {
            haiku.Ratings.Clear();
            haiku.DatePublished = DateTime.Now;
            this.haikus.Update(haiku);
            this.haikus.SaveChanges();
        }
    }
}