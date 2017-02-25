using StudentPortal.Domain.Context;
using StudentPortal.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace StudentPortal.Controllers
{
    public class BaseApplicationController : Controller
    {
        protected readonly StudentPortalContext _ctx = new StudentPortalContext();

        public BaseApplicationController()
        {
            this.SetupViewbag();
        }

        /// <summary>
        /// Configure the viewbag to have basic information that is used across multiple controllers 
        /// such as institution settings etc.
        /// Optionally you can override this method in the controllers to provide additional information
        /// to the views.
        /// </summary>
        protected virtual void SetupViewbag()
        {
            Institution institution = _ctx.Institutions.FirstOrDefault();
            if (institution != null)
            {
                ViewBag.Institution = institution.Name;
                ViewBag.ContactPhone = institution.ContactNumber;
                ViewBag.ContactEmail = institution.ContactEmail;
            }
        }
    }
}