using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class GRO
    {
        //get dashboard elements
        public static ViewModels.DashboardElements getDashboard()
        {
            ViewModels.DashboardElements toReturn=new ViewModels.DashboardElements();
            using(var db=new DLL.Model.Context())
            {
                string date = DateTime.Now.Date.ToString();
                DateTime date0=DateTime.Parse(date);
                DateTime date1 = date0.AddDays(1).Date;
                DateTime date2 = date0.AddDays(2).Date;
                DateTime date3 = date0.AddDays(3).Date;
                DateTime date4 = date0.AddDays(4).Date;
                DateTime date5 = date0.AddDays(5).Date;
                DateTime date6 = date0.AddDays(6).Date;
               toReturn.visitorDay1= db.previsitors.Where(m => m.Date.Equals(date0) && m.status == true).Count();
               toReturn.visitorDay2 = db.previsitors.Where(m => m.Date.Equals(date1) && m.status == true).Count();
               toReturn.visitorDay3 = db.previsitors.Where(m => m.Date.Equals(date2) && m.status == true).Count();
               toReturn.visitorDay4 = db.previsitors.Where(m => m.Date.Equals(date3) && m.status == true).Count();
               toReturn.visitorDay5 = db.previsitors.Where(m => m.Date.Equals(date4) && m.status == true).Count();
               toReturn.visitorDay6 = db.previsitors.Where(m => m.Date.Equals(date5) && m.status == true).Count();
               toReturn.visitorDay7 = db.previsitors.Where(m => m.Date.Equals(date6) && m.status == true).Count();
            }
            return toReturn;
        }

        #region GetVisitor
        
        public static List<BLL.ViewModels.PreVisitors> getPreVisitors()
        {
            List<BLL.ViewModels.PreVisitors> toreturn = new List<ViewModels.PreVisitors>();
            List<DLL.Model.PreVisitors> entity = new List<DLL.Model.PreVisitors>();
            try
            {
                using (var db = new DLL.Model.Context())
                {
                    entity = db.previsitors.Where(m=>m.status == true).ToList();
                int entityCount = entity.Count();
                for (int i = 0; i < entityCount; i++)
                {
                    int userId=int.Parse(entity[i].CreatedBy);
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
                        CreatedBy =db.Users.Where(m=>m.UsersId.Equals(userId)).FirstOrDefault().name,
                        host_name = entity[i].hFirstName + " " + entity[i].hLastName,
                        // hfloor = db.Company.Where(m=>m.company_id.Equals(hostdetail.Company_ID)).FirstOrDefault().FloorID,
                        host_company = entity[i].hcompany,
                        status = true,

                    });
                }
                }
            }
            catch (Exception ex)
            {
                return null;
            }
            return toreturn;
        }
        // for to and from date values
        public static List<BLL.ViewModels.PreVisitors> getPreVisitors(string from_date, string to_date)
        {
            List<BLL.ViewModels.PreVisitors> toreturn = new List<ViewModels.PreVisitors>();
            List<DLL.Model.PreVisitors> entity = new List<DLL.Model.PreVisitors>();
            try
            {
                DateTime from = from_date!=null || from_date!=""? DateTime.Parse(from_date):DateTime.Parse("1999-01-01");
                DateTime to = to_date != null || to_date != "" ? DateTime.Parse(to_date) : DateTime.Parse("1999-01-01");
                if ((from_date == null || from_date == "") || (to_date == null || to_date == ""))
                {
                    return getPreVisitors();
                }
                using (var db = new DLL.Model.Context())
                {
                   
                    entity = db.previsitors.Where(m => m.status == true && m.Date>= from && m.Date <=to).ToList();
                    int entityCount = entity.Count();
                    for (int i = 0; i < entityCount; i++)
                    {
                        int userId = int.Parse(entity[i].CreatedBy);
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
                            CreatedBy = db.Users.Where(m => m.UsersId.Equals(userId)).FirstOrDefault().name,
                            host_name = entity[i].hFirstName + " " + entity[i].hLastName,
                            // hfloor = db.Company.Where(m=>m.company_id.Equals(hostdetail.Company_ID)).FirstOrDefault().FloorID,
                            host_company = entity[i].hcompany,
                            status = true,

                        });
                    }
                }
            }
            catch (Exception ex)
            {
                return null;
            }
            return toreturn;
        }
        #endregion
    }
}
