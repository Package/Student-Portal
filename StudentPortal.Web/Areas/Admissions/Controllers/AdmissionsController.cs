using StudentPortal.Controllers;
using StudentPortal.Domain.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace StudentPortal.Areas.Admissions.Controllers
{
    [Authorize(Roles = "Admin, Admissions")]
    public class AdmissionsController : BaseApplicationController
    {
        public ActionResult Home()
        {
            return View();
        }
    }
}