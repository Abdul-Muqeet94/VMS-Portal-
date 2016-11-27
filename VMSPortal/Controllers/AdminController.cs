using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace VMSPortal.Controllers
{
    [Authorize(Roles = BLL.VMSRoles.ROLE_ADMIN)]
    public class AdminController : Controller
    {
        //
        // GET: /Admin/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult add()
        {
            return View(BLL.Admin.getCreateUser());
        }
        [HttpPost]
        public ActionResult add(BLL.ViewModels.Users fromForm)
        {
            BLL.Admin.addUser(fromForm);
            return RedirectToAction("add");
        }
        public ActionResult ViewUsers()
        {
            return View(BLL.Admin.allUsers());
        }
        public ActionResult EditUser(int id)
        {
            return View(BLL.Admin.getUserById(id));
        }
        [HttpPost]
        public ActionResult EditUser(BLL.ViewModels.Users User)
        {
            BLL.Admin.editVisitor(User);
            return RedirectToAction("ViewUsers");
        }
        public ActionResult DeleteUser(int id)
        {
            BLL.Admin.deleteUser(id);
            return RedirectToAction("ViewUsers");
        }



        


    }
}
