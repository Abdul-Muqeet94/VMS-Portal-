using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class PreVisitors
    {

        #region get visitors
        public static BLL.ViewModels.CreatePreVisitors getHosts(string companyId)
        {
            List<DLL.Model.Hosts> hosts = new List<DLL.Model.Hosts>();
            try
            {

                using (var db = new DLL.Model.Context())
                {
                    hosts = db.Hosts.Where(m => m.Company_ID.Equals(companyId)).ToList();
                }
            }
            catch
            {
                hosts = null;
            }
            int totalHosts = hosts.Count();
            BLL.ViewModels.CreatePreVisitors toReturn = new ViewModels.CreatePreVisitors();
            for (int i = 0; i < totalHosts; i++)
            {
                toReturn.hosts.Add(new BLL.ViewModels.Hosts
                    {
                        Company_ID = hosts[i].Company_ID,
                        EmpCode = hosts[i].EmpCode,
                        hFirst = hosts[i].hFirst,
                        hLast = hosts[i].hLast,
                        EmpID = hosts[i].EmpID
                    });
            }
            return toReturn;
        }
        public static BLL.ViewModels.Users getUser(string name)
        {
            BLL.ViewModels.Users toreturn = new ViewModels.Users();
            try
            {
                using (var db = new DLL.Model.Context())
                {

                    var entity = db.Users.Where(m => m.name.Equals(name) && m.active == true).FirstOrDefault();
                    toreturn.acessgroup_id = entity.accessGroup.AccessGroupId;
                    toreturn.active = entity.active;
                    toreturn.company_id = entity.company.company_id;
                    toreturn.name = entity.name;
                    toreturn.password = entity.password;
                    toreturn.userId = entity.UsersId;
                }
            }
            catch (Exception ex)
            {
                toreturn = null;
            }
            return toreturn;
        }
        
        public static List<BLL.ViewModels.PreVisitors> getPreVisitors(string name)
        {
            List<BLL.ViewModels.PreVisitors> toreturn = new List<ViewModels.PreVisitors>();
            List<DLL.Model.PreVisitors> entity = new List<DLL.Model.PreVisitors>();
            try
            {
                using (var db = new DLL.Model.Context())
                {
                    var user = db.Users.Where(m => m.name.Equals(name)).FirstOrDefault();
                    string id = user.UsersId.ToString();
                    entity = db.previsitors.Where(m => m.CreatedBy.Equals(id) && m.status == true).ToList();
                }


                int entityCount = entity.Count();
                for (int i = 0; i < entityCount; i++)
                {
                    toreturn.Add(new ViewModels.PreVisitors
                    {
                        ID = entity[i].ID,
                        VNIC = entity[i].VNIC,
                        Vaddress = entity[i].Vaddress,
                        Vcompany = entity[i].Vcompany,
                        VfirstName = entity[i].VfirstName,
                        VlastName = entity[i].VlastName,
                        VphoneNo = entity[i].VphoneNo,
                        Date = entity[i].Date,
                        Time = entity[i].Time,
                        host_email = entity[i].hemail,
                        CreatedBy = entity[i].CreatedBy,
                        host_name = entity[i].hFirstName + " " + entity[i].hLastName,
                        // hfloor = db.Company.Where(m=>m.company_id.Equals(hostdetail.Company_ID)).FirstOrDefault().FloorID,
                        host_company = entity[i].hcompany,
                        status = true
                    });
                }
            }
            catch (Exception ex)
            {
                return null;
            }
            return toreturn;
        }

        // for to and from date time View
        public static List<BLL.ViewModels.PreVisitors> getPreVisitors(string name,string from_date, string to_date)
        {
            List<BLL.ViewModels.PreVisitors> toreturn = new List<ViewModels.PreVisitors>();
            List<DLL.Model.PreVisitors> entity = new List<DLL.Model.PreVisitors>();
            try
            {
                DateTime from = DateTime.Parse(from_date);
                DateTime to = DateTime.Parse(to_date);
                using (var db = new DLL.Model.Context())
                {
                    var user = db.Users.Where(m => m.name.Equals(name)).FirstOrDefault();
                    string id = user.UsersId.ToString();
                    entity = db.previsitors.Where(m => m.CreatedBy.Equals(id) && m.Date>=from && m.Date <=to && m.status == true).ToList();
                }


                int entityCount = entity.Count();
                for (int i = 0; i < entityCount; i++)
                {
                    toreturn.Add(new ViewModels.PreVisitors
                    {
                        ID = entity[i].ID,
                        VNIC = entity[i].VNIC,
                        Vaddress = entity[i].Vaddress,
                        Vcompany = entity[i].Vcompany,
                        VfirstName = entity[i].VfirstName,
                        VlastName = entity[i].VlastName,
                        VphoneNo = entity[i].VphoneNo,
                        Date = entity[i].Date,
                        Time = entity[i].Time,
                        host_email = entity[i].hemail,
                        CreatedBy = entity[i].CreatedBy,
                        host_name = entity[i].hFirstName + " " + entity[i].hLastName,
                        // hfloor = db.Company.Where(m=>m.company_id.Equals(hostdetail.Company_ID)).FirstOrDefault().FloorID,
                        host_company = entity[i].hcompany,
                        status = true
                    });
                }
            }
            catch (Exception ex)
            {
                return null;
            }
            return toreturn;
        }
        public static BLL.ViewModels.EditViewModel getPreVisitorsById(int id)
        {
            BLL.ViewModels.EditViewModel toreturn = null;
            DLL.Model.PreVisitors entity = new DLL.Model.PreVisitors();
            string company_id = null;
            try
            {
                using (var db = new DLL.Model.Context())
                {

                    entity = db.previsitors.Where(m => m.ID.Equals(id) && m.status == true).FirstOrDefault();
                    string name = entity.hcompany;
                    var company = db.Company.Where(m => m.CompanyName.Equals(name)).FirstOrDefault();
                    company_id = company.company_id;
                }
                toreturn = new BLL.ViewModels.EditViewModel
                {
                    ID = entity.ID,
                    VNIC = entity.VNIC,
                    Vaddress = entity.Vaddress,
                    Vcompany = entity.Vcompany,
                    VfirstName = entity.VfirstName,
                    VlastName = entity.VlastName,
                    VphoneNo = entity.VphoneNo,
                    host_name = entity.hFirstName + " " + entity.hLastName,
                    host_email = entity.hemail,
                    date = entity.Date.ToString(),
                    time = entity.Time,
                    // hfloor = db.Company.Where(m=>m.company_id.Equals(hostdetail.Company_ID)).FirstOrDefault().FloorID,
                    hosts = BLL.PreVisitors.getHosts(company_id).hosts,
                };
            }
            catch (Exception ex)
            {
                return null;
            }
            return toreturn;
        }
        #endregion



        public static int editVisitor(BLL.ViewModels.PreVisitors visitor)
        {
            try
            {
                using (var db = new DLL.Model.Context())
                {
                    var entity = db.previsitors.Find(visitor.ID);
                    var hostdetail = db.Hosts.Find(visitor.host_id);
                    if (entity != null)
                    {
                        entity.VNIC = visitor.VNIC;
                        entity.Vaddress = visitor.Vaddress;
                        entity.Vcompany = visitor.Vcompany;
                        entity.VfirstName = visitor.VfirstName;
                        entity.VlastName = visitor.VlastName;
                        entity.VphoneNo = visitor.VphoneNo;
                        entity.hemail = visitor.host_email;
                        entity.Time = visitor.Time;
                        entity.hFirstName = hostdetail.hFirst;
                        entity.hLastName = hostdetail.hLast;
                        entity.Date = DateTime.ParseExact(visitor.from_Date, "d-M-yyyy", CultureInfo.InvariantCulture);
                        // hfloor = db.Company.Where(m=>m.company_id.Equals(hostdetail.Company_ID)).FirstOrDefault().FloorID,
                        entity.hcompany = db.Company.Where(m => m.company_id.Equals(hostdetail.Company_ID)).FirstOrDefault().CompanyName;
                        entity.status = true;
                        db.SaveChanges();
                        return entity.ID;
                    }
                    else
                    {
                        return -1;
                    }
                }
            }
            catch (Exception ex)
            {
                return -1;
            }


        }
        public static int deleteVisitor(int id)
        {
            using (var db = new DLL.Model.Context())
            {
                var visitor = db.previsitors.Find(id);
                if (visitor != null)
                {
                    visitor.status = false;
                    db.SaveChanges();
                    return id;
                }
                else
                {
                    return -1;
                }
            }
        }
        #region Dashboard
        public static BLL.ViewModels.DashboardElements getDashboard(string name)
        {
            ViewModels.DashboardElements toReturn = new ViewModels.DashboardElements();
            using(var db=new DLL.Model.Context())
            {
                string date = DateTime.Now.Date.ToString();
                DateTime date0 = DateTime.Parse(date);
                DateTime date1 = date0.AddDays(1).Date;
                DateTime date2 = date0.AddDays(2).Date;
                DateTime date3 = date0.AddDays(3).Date;
                DateTime date4 = date0.AddDays(4).Date;
                DateTime date5 = date0.AddDays(5).Date;
                DateTime date6 = date0.AddDays(6).Date;
                string createdBy=db.Users.Where(m=>m.name.Equals(name)).FirstOrDefault().UsersId.ToString();
                toReturn.visitorDay1 = db.previsitors.Where(m => m.Date.Equals(date0) && m.CreatedBy==createdBy && m.status == true).Count();
                toReturn.visitorDay2 = db.previsitors.Where(m => m.Date.Equals(date1) && m.CreatedBy == createdBy && m.status == true).Count();
                toReturn.visitorDay3 = db.previsitors.Where(m => m.Date.Equals(date2) && m.CreatedBy == createdBy && m.status == true).Count();
                toReturn.visitorDay4 = db.previsitors.Where(m => m.Date.Equals(date3) && m.CreatedBy == createdBy && m.status == true).Count();
                toReturn.visitorDay5 = db.previsitors.Where(m => m.Date.Equals(date4) && m.CreatedBy == createdBy && m.status == true).Count();
                toReturn.visitorDay6 = db.previsitors.Where(m => m.Date.Equals(date5) && m.CreatedBy == createdBy && m.status == true).Count();
                toReturn.visitorDay7 = db.previsitors.Where(m => m.Date.Equals(date6) && m.CreatedBy == createdBy && m.status == true).Count();

            }
            return toReturn;
        }
        #endregion

        #region Add Visitors
        public static int addVisitor(BLL.ViewModels.PreVisitors visitor)
        {
            try
            {
                using (var db = new DLL.Model.Context())
                {
                    var hostdetail = db.Hosts.Find(visitor.host_id);
                    DateTime from = DateTime.ParseExact(visitor.from_Date, "d-M-yyyy", CultureInfo.InvariantCulture);
                    DateTime to = DateTime.ParseExact(visitor.to_date, "d-M-yyyy", CultureInfo.InvariantCulture);
                    int totaldays = to.Subtract(from).Days;
                    for (int i = 0; i <= totaldays; i++)
                    {
                        DateTime check = from.AddDays(i);
                        db.previsitors.Add(new DLL.Model.PreVisitors
                        {
                            VNIC = visitor.VNIC,
                            Vaddress = visitor.Vaddress,
                            Vcompany = visitor.Vcompany,
                            VfirstName = visitor.VfirstName,
                            VlastName = visitor.VlastName,
                            VphoneNo = visitor.VphoneNo,
                            Date = from.AddDays(i),
                            Time = visitor.Time,
                            hemail = visitor.host_email,
                            CreatedBy = visitor.CreatedBy,
                            hFirstName = hostdetail.hFirst,
                            hLastName = hostdetail.hLast,
                            // hfloor = db.Company.Where(m=>m.company_id.Equals(hostdetail.Company_ID)).FirstOrDefault().FloorID,
                            hcompany = db.Company.Where(m => m.company_id.Equals(hostdetail.Company_ID)).FirstOrDefault().CompanyName,
                            status = true
                        });
                    }
                    db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                return -1;
            }
            return visitor.ID;
        }
        public static bool addMultipleVisitor(List<BLL.ViewModels.AddMultipleVisitor> previsitors)
        {
            try
            {

                using (var db = new DLL.Model.Context())
                {
                    foreach (var visitor in previsitors)
                    {
                        var hostdetail = db.Hosts.Find(visitor.host_id);
                        DateTime from = DateTime.ParseExact(visitor.from_Date, "d-M-yyyy", CultureInfo.InvariantCulture);
                        DateTime to = DateTime.ParseExact(visitor.to_date, "d-M-yyyy", CultureInfo.InvariantCulture);
                        int totaldays = to.Subtract(from).Days;
                        for (int i = 0; i <= totaldays; i++)
                        {
                            DateTime check = from.AddDays(i);
                            db.previsitors.Add(new DLL.Model.PreVisitors
                            {
                                VNIC = visitor.VNIC,
                                Vaddress = visitor.Vaddress,
                                Vcompany = visitor.Vcompany,
                                VfirstName = visitor.VfirstName,
                                VlastName = visitor.VlastName,
                                VphoneNo = visitor.VphoneNo,
                                Date = from.AddDays(i),
                                Time = visitor.Time,
                                hemail = visitor.host_email,
                                CreatedBy = visitor.CreatedBy,
                                hFirstName = hostdetail.hFirst,
                                hLastName = hostdetail.hLast,
                                // hfloor = db.Company.Where(m=>m.company_id.Equals(hostdetail.Company_ID)).FirstOrDefault().FloorID,
                                hcompany = db.Company.Where(m => m.company_id.Equals(hostdetail.Company_ID)).FirstOrDefault().CompanyName,
                                status = true
                            });
                        }

                    }
                    db.SaveChanges();
                }

            }
            catch (Exception ex)
            {
                return false;
            }
            return true;
        }
        #endregion
    }
}