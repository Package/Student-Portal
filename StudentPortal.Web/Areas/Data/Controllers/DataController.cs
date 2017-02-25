using StudentPortal.Controllers;
using StudentPortal.Domain.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace StudentPortal.Areas.Data.Controllers
{
    [Authorize(Roles = "Admin, Data")]
    public class DataController : BaseApplicationController
    {
        public ActionResult Home()
        {
            return View();
        }
    }
}