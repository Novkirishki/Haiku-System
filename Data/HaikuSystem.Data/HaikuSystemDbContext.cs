namespace HaikuSystem.Data
{
    using Microsoft.AspNet.Identity.EntityFramework;
    using Models;

    public class HaikuSystemDbContext : IdentityDbContext<User>
    {
        public HaikuSystemDbContext() : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static HaikuSystemDbContext Create()
        {
            return new HaikuSystemDbContext();
        }
    }
}
