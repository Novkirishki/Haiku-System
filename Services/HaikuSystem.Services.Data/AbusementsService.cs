namespace HaikuSystem.Services.Data
{
    using HaikuSystem.Services.Data.Contracts;
    using HaikuSystem.Data.Models;
    using System.Linq;
    using HaikuSystem.Data.Repositories;

    public class AbusementsService : IAbusementsService
    {
        private IRepository<Abusement> abusements;

        public AbusementsService(IRepository<Abusement> abusements)
        {
            this.abusements = abusements;
        }

        public IQueryable<Abusement> GetAll(int skip, int take)
        {
            return this.abusements.All().OrderBy(ab => ab.CreatedOn).Skip(skip).Take(take);
        }
    }
}
