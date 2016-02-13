namespace HaikuSystem.Services.Data.Contracts
{
    using HaikuSystem.Data.Models;
    using System.Linq;
    public interface IHaikusService
    {
        Haiku AddHaiku(string text, int userId);

        void UpdateHaiku(Haiku haiku);

        void Delete(Haiku haiku);

        void DeleteById(int id);

        IQueryable<Haiku> GetById(int id);

        IQueryable<Haiku> GetAll(string sortBy, string sortType, int skip, int take);

        void Rate(int haikuId, int rating);

        void DeleteAll(User user);

        void Abuse(int haikuId, string text);
    }
}
