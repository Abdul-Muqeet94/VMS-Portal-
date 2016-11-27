using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace DLL.Model
{
    public class Context:DbContext
    {
        public static string connectionString = ConfigurationManager.ConnectionStrings["VMS"].ConnectionString;
        public Context()
            : base(connectionString)
        {

        }
        public DbSet<Company> Company { get; set; }
        public DbSet<AccessGroup> AccessGroup { get;set;}
        public DbSet<Users> Users { get; set; }
        public DbSet<PreVisitors> previsitors { get; set; }
        public DbSet<Hosts> Hosts { get; set; }
    }
}
