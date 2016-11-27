using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace VMSPortal.Controllers
{
    [Authorize(Roles = BLL.VMSRoles.ROLE_GRO)]
    public class GROController : Controller
    {
        //
        // GET: /GRO/

        public ActionResult GRO()
        {
            
            return View(BLL.GRO.getDashboard());
        }
        public ActionResult ViewVisitor()
        {
            return View(BLL.GRO.getPreVisitors());
        }
        [HttpPost]
        public ActionResult ViewVisitor(string from_date, string to_date)
        {
            return View(BLL.GRO.getPreVisitors(from_date,to_date));
        }

    }
}
