using DLL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class Admin
    {
        public static ViewModels.CreateUser getCreateUser()
        {
            List<AccessGroup> accessGroups = null;
            List<Company> company = null;
            using (Context db = new Context())
            {
                try
                {
                    accessGroups = db.AccessGroup.ToList();
                    company = db.Company.ToList();
                }
                catch (System.Data.Entity.Core.EntityCommandExecutionException ex)
                {
                    accessGroups = new List<AccessGroup>();
                    company = new List<Company>();
                }
                ViewModels.CreateUser createUser = new ViewModels.CreateUser();
                List<ViewModels.AccessGroup> ac = new List<ViewModels.AccessGroup>();
                for (int i = 0; i < accessGroups.Count(); i++)
                {
                    ac.Add(new ViewModels.AccessGroup()
                    {
                        name = accessGroups[i].name,
                        accessGroupId = accessGroups[i].AccessGroupId,
                        active = accessGroups[i].active
                    });

                }
                List<ViewModels.Company> co = new List<ViewModels.Company>();
                for (int i = 0; i < company.Count(); i++)
                {
                    co.Add(new ViewModels.Company()
                    {
                        company_id = company[i].company_id,
                        CompanyID = company[i].CompanyID,
                        BuildingCode = company[i].BuildingCode,
                        CompanyName = company[i].CompanyName,
                        FloorID = company[i].FloorID
                    });

                }
                createUser.company = co;
                createUser.accessGroup = ac;

                return createUser;
            }
        }

        public static int addUser(BLL.ViewModels.Users user)
        {
            try
            {
                DLL.Model.Users us = new DLL.Model.Users();
                using (Context db = new Context())
                {
                    us.name = user.name;
                    us.accessGroup = db.AccessGroup.Find(user.acessgroup_id);
                    us.active = true;
                    us.company = db.Company.Find(int.Parse(user.company_id));
                    
                    DLL.Commons.Passwords.setPassword(us, user.password);
                    db.Users.Add(us);
                    db.SaveChanges();
                }

                return user.userId;
            }
            catch (Exception ex)
            {
                return -1;
            }

        }

        public static BLL.ViewModels.Users getUser(string name)
        {
            BLL.ViewModels.Users toReturn = new ViewModels.Users();
            try
            {
                using (var db = new Context())
                {
                    var entity = db.Users.Where(m => m.name.Equals(name)).FirstOrDefault();
                    toReturn.company_id = entity.company.company_id;
                    toReturn.name = entity.name;
                    toReturn.password = entity.password;
                    toReturn.userId = entity.UsersId;
                    toReturn.active = entity.active;
                    toReturn.acessgroup_id = entity.accessGroup.AccessGroupId;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
            return toReturn;
        }

        public static List<BLL.ViewModels.Users> allUsers()
        {
            List<BLL.ViewModels.Users> toReturn = new List<ViewModels.Users>();
            try
            {
                using (var db = new DLL.Model.Context())
                {
                    var UsersList = db.Users.Where(m => m.active).ToList();
                    int count = UsersList.Count();
                    for (int i = 0; i < count; i++)
                    {
                        toReturn.Add(new BLL.ViewModels.Users()
                        {
                            accessgroup_name = UsersList[i].accessGroup!=null?UsersList[i].accessGroup.name:null,
                            compnay_name = UsersList[i].company!=null?(UsersList[i].company.CompanyName):null,
                            userId = UsersList[i].UsersId,
                            name = UsersList[i].name
                        }
                         );
                    }
                }
            }
            catch (Exception ex)
                {
                    return toReturn;
                }

            return toReturn;
        }

        public static BLL.ViewModels.Users getUserById(int id)
        {
            BLL.ViewModels.Users toReturn= null;
            using (var db=new Context())
            {
                var user=db.Users.Find(id);
                toReturn = new ViewModels.Users
                {
                    name = user.name,
                    create_user = getCreateUser(),
                    compnay_name = user.company.CompanyName,
                    accessgroup_name = user.accessGroup.name,
                    userId = id
                };
            }
            return toReturn;
        }

        public static int editVisitor(BLL.ViewModels.Users user)
        {
            try
            {
                using (var db = new DLL.Model.Context())
                {
                    var User = db.Users.Find(user.userId);
                    User.accessGroup = db.AccessGroup.Find(user.acessgroup_id);
                    User.company = db.Company.Find(int.Parse(user.company_id));
                    User.name = user.name;
                    DLL.Commons.Passwords.setPassword(User, user.password);
                    db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                return 0;
            }
            return 1;
        }

        public static void deleteUser(int id)
        {
            using (var db=new Context())
            {
                var user=db.Users.Find(id);
                user.active = false;
                db.SaveChanges();
            }
        }
    }
}
