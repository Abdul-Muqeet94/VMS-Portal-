using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace VMSPortal.Controllers
{
    [Authorize(Roles = BLL.VMSRoles.ROLE_TENANT)]
    public class PreVisitorController : Controller
    {
        //
        // GET: /PreVisitor/

        public ActionResult PreVisitor()
        {
            return View(BLL.PreVisitors.getDashboard(User.Identity.Name));
        }

        public ActionResult AddPreVisitor()
        {
            return View(BLL.PreVisitors.getHosts(BLL.PreVisitors.getUser(User.Identity.Name).company_id));
        }

        [HttpPost]
        public ActionResult AddPreVisitor(BLL.ViewModels.PreVisitors visitor)
        {
            visitor.CreatedBy= BLL.Admin.getUser(User.Identity.Name).userId.ToString();
            BLL.PreVisitors.addVisitor(visitor);
            return RedirectToAction("AddPreVisitor");
        }
        public ActionResult addMultipleVisitor()
        {
            string companyId=BLL.PreVisitors.getUser(User.Identity.Name).company_id;

            List<BLL.ViewModels.AddMultipleVisitor> visitorList = new List<BLL.ViewModels.AddMultipleVisitor>{
                new BLL.ViewModels.AddMultipleVisitor{
                    VNIC="",
                    VfirstName=""
                }
            };


            visitorList[0].hosts = BLL.PreVisitors.getHosts(companyId).hosts;
            return View(visitorList);
        }
        [HttpPost]
        public ActionResult addMultipleVisitor(List<BLL.ViewModels.AddMultipleVisitor> PreVisitors)
        {
            BLL.PreVisitors.addMultipleVisitor(PreVisitors);
            return RedirectToAction("addMultipleVisitor");
        }
        public ActionResult ViewVisitor()
        {
            return View(BLL.PreVisitors.getPreVisitors(User.Identity.Name));
        }
        [HttpPost]
        public ActionResult ViewVisitor(string from_date, string to_date)
        {
            return View(BLL.PreVisitors.getPreVisitors(User.Identity.Name, from_date, to_date));
        }
        public ActionResult EditVisitor(int id)
        {
            return View(BLL.PreVisitors.getPreVisitorsById(id));
        }
        [HttpPost]
        public ActionResult EditVisitor(BLL.ViewModels.PreVisitors visitor)
        {
            BLL.PreVisitors.editVisitor(visitor);
            return RedirectToAction("ViewVisitor");
        }
        public ActionResult DeleteVisitor(int id)
        {
            BLL.PreVisitors.deleteVisitor(id);
            
            return RedirectToAction("ViewVisitor");
        }
    }
}
