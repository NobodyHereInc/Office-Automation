using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using OA.Model;

namespace OA.DAL
{
    public class OAContext : DbContext
    {
        public OAContext() : base()
        {

        }

        public OAContext(DbContextOptions<OAContext> options)
            : base(options)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Data Source=.;Initial Catalog=OA_DB;Integrated Security=True;Connect Timeout=15;Encrypt=False;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
        }

        public DbSet<UserInfo> UserInfo { get; set; }
    }
}
