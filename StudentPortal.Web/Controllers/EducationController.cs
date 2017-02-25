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
    public class EducationController : BaseApplicationController
    {
        private readonly IApplicationService _applicationService;

        public EducationController(IApplicationService _applicationService) : base()
        {
            this._applicationService = _applicationService;
        }

        public async Task<ActionResult> Default(int? id)
        {
            EducationHistory educationHistory = await _applicationService.GetEducationHistory(_ctx)
                ?? new EducationHistory();

            ViewBag.Schools = await _ctx.Schools.ToListAsync();
            return View(educationHistory);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Default(EducationHistory educationHistory)
        {
            Application application = await _applicationService.GetCurrentApplication(_ctx);

            if (ModelState.IsValid)
            {
                educationHistory.Application = application;

                EducationHistory currentEducation = await _applicationService.GetEducationHistory(_ctx);
                if (currentEducation != null)
                {
                    educationHistory.Id = currentEducation.Id;
                }

                _ctx.Education.AddOrUpdate(a => a.Id, educationHistory);
                await _ctx.SaveChangesAsync();

                return RedirectToAction("Manage", "Applications");
            }

            ViewBag.Schools = await _ctx.Schools.ToListAsync();
            return View(educationHistory);
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
