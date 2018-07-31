using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;

namespace Geek_Registration_System.Models
{
    public class GRSDBContext : DbContext
    {
        public GRSDBContext()
        : base("GRSDBContext")
        {
            //open or close the initializer function
            //Database.SetInitializer<Geek_Registration_System.Models.GRSDBContext>(null);
        }
        public DbSet<Candidate> Candidates { get; set; }
        public DbSet<Skill> Skills { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            base.OnModelCreating(modelBuilder);
            var instance = System.Data.Entity.SqlServer.SqlProviderServices.Instance;
        }
    }
}