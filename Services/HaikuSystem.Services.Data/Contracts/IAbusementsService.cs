namespace HaikuSystem.Services.Data.Contracts
{
    using HaikuSystem.Data.Models;
    using System.Linq;

    public interface IAbusementsService
    {
        IQueryable<Abusement> GetAll(int skip, int take);
    }
}
