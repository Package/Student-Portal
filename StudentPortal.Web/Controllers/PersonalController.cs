using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using StudentPortal.Domain.Context;
using StudentPortal.Domain.Models;
using StudentPortal.Services.Interfaces;

namespace StudentPortal.Controllers
{
    [Authorize]
    public class PersonalController : BaseApplicationController
    {
        private readonly IApplicationService _applicationService;

        public PersonalController(
            IApplicationService _applicationService) : base()
        {
            this._applicationService = _applicationService;
        }

        public async Task<ActionResult> Default()
        {
            PersonalDetails personalDetails = await _applicationService.GetPersonalDetails(_ctx) 
                ?? new PersonalDetails();

            SetupViewbag();

            return View(personalDetails);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Default(PersonalDetails personalDetails)
        {
            Application application = await _applicationService.GetCurrentApplication(_ctx);

            if (ModelState.IsValid)
            {
                personalDetails.Application = application;

                PersonalDetails currentDetails = await _applicationService.GetPersonalDetails(_ctx);
                if (currentDetails != null)
                {
                    personalDetails.Id = currentDetails.Id;
                }

                _ctx.PersonalDetails.AddOrUpdate(a => a.Id, personalDetails);
                await _ctx.SaveChangesAsync();

                return RedirectToAction("Manage", "Applications");
            }
 
            SetupViewbag();

            return View(personalDetails);
        }

        protected override void SetupViewbag()
        {
            base.SetupViewbag();

            ViewBag.Titles = _ctx.Titles.ToList();
            ViewBag.Genders = _ctx.Genders.ToList();
            ViewBag.Countries = _ctx.Countries.ToList();
            ViewBag.Ethnicities = _ctx.Ethnicity.ToList();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _ctx.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
