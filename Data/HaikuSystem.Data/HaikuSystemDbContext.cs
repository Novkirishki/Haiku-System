﻿namespace HaikuSystem.Data
{
    using Models;
    using System.Data.Entity;
    using System.Data.Entity.ModelConfiguration.Conventions;

    public class HaikuSystemDbContext : DbContext
    {
        public HaikuSystemDbContext() : base("DefaultConnection")
        {
        }

        public virtual IDbSet<User> Users { get; set; }

        public virtual IDbSet<Haiku> Haikus { get; set; }

        public virtual IDbSet<Rating> Ratings { get; set; }

        public virtual IDbSet<Abusement> Abusements { get; set; }

        public static HaikuSystemDbContext Create()
        {
            return new HaikuSystemDbContext();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
