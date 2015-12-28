namespace HaikuSystem.Web.Api
{
    using System.Data.Entity;
    using HaikuSystem.Data;
    using HaikuSystem.Data.Migrations;

    public static class DatabaseConfig
    {
        public static void Initialize()
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<HaikuSystemDbContext, Configuration>());
        }
    }
}