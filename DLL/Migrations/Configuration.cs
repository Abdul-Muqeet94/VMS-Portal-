namespace DLL.Migrations
{
    using DLL.Model;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<DLL.Model.Context>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(DLL.Model.Context db)
        {
            if (db.AccessGroup.Where(m => m.name.Equals(Commons.Roles.ROLE_ADMIN)).FirstOrDefault() == null)
            {

                db.AccessGroup.AddOrUpdate(
                    new AccessGroup { name = Commons.Roles.ROLE_ADMIN, active = true },
                    new AccessGroup { name = Commons.Roles.ROLE_TENANT, active = true },
                    new AccessGroup { name = Commons.Roles.ROLE_GRO, active = true }
            );
                
                   
            }
            if(db.Users.Where(m=>m.name.Equals("admin")).FirstOrDefault()==null)
            {
                Users u1=new Users
                {
                    name="admin",
                    accessGroup=db.AccessGroup.Where(m=>m.name.Equals(DLL.Commons.Roles.ROLE_ADMIN)).FirstOrDefault(),
                    active=true
                };
                DLL.Commons.Passwords.setPassword(u1, "admin");
                db.Users.Add(u1);

                db.SaveChanges();
            }
            var u2=db.Users.Where(m=>m.name.Equals("admin")).FirstOrDefault();
            u2.accessGroup=db.AccessGroup.Where(m=>m.name.Equals(DLL.Commons.Roles.ROLE_ADMIN)).FirstOrDefault();
            db.SaveChanges();
        }
    }
}
