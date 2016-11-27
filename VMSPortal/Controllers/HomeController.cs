using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BLL;

namespace VMSPortal.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            
           if(User.IsInRole(BLL.VMSRoles.ROLE_ADMIN))
           {
               return RedirectToRoute(new
               {
                   Controller = "Admin",
                   Action = "Index"
               });
           }
                else if(User.IsInRole(BLL.VMSRoles.ROLE_TENANT))
           {
               return RedirectToRoute(new
               {
                   Controller = "PreVisitor",
                   Action = "PreVisitor"
               });
           }
           else if (User.IsInRole(BLL.VMSRoles.ROLE_GRO))
           {
               return RedirectToRoute(new
               {
                   Controller = "GRO",
                   Action = "GRO"
               });
           }
           else
           {
               return RedirectToRoute(new
               {
                   Controller = "Account",
                   Action = "Login"
               });
           }
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your app description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}
