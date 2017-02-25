using StudentPortal.Controllers;
using StudentPortal.Domain.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace StudentPortal.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : BaseApplicationController
    {
        public ActionResult Home()
        {
            return View();
        }
    }
}