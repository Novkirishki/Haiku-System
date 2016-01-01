namespace HaikuSystem.Services.Data.Contracts
{
    using HaikuSystem.Data.Models;
    using System.Linq;
    public interface IUsersService
    {
        void Register(string username, string publishCode);

        IQueryable<User> GetByUsername(string username);

        IQueryable<User> GetByPublishCode(string publishCode);

        IQueryable<User> GetAll(string sortBy, string sortType, int skip, int take);
    }
}
