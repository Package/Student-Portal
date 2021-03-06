﻿using StudentPortal.Controllers;
using StudentPortal.Domain.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace StudentPortal.Areas.Reporting.Controllers
{
    [Authorize(Roles = "Admin, Data, SMT, Admissions, Staff")]
    public class ReportingController : BaseApplicationController
    {
        public ActionResult Home()
        {
            return View();
        }
    }
}