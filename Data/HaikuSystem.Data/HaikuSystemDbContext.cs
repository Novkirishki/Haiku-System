namespace HaikuSystem.Data
{
    using System.Data.Entity;

    public class HaikuSystemDbContext : DbContext
    {
        public HaikuSystemDbContext() : base("DefaultConnection")
        {
        }

        public static HaikuSystemDbContext Create()
        {
            return new HaikuSystemDbContext();
        }
    }
}
