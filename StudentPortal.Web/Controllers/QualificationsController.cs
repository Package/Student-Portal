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
    public class QualificationsController : BaseApplicationController
    {
        private readonly IApplicationService _applicationService;

        public QualificationsController(IApplicationService _applicationService) : base()
        {
            this._applicationService = _applicationService;
        }

        public async Task<ActionResult> Default()
        {
            SetupViewbag();
            ViewBag.Qualifications = await _applicationService.GetQualifications(_ctx);

            return View(new Qualification());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Default([Bind(Exclude="Id")]Qualification qualification)
        {
            if (ModelState.IsValid)
            {
                qualification.Application = await _applicationService.GetCurrentApplication(_ctx);

                _ctx.Qualifications.Add(qualification);
                await _ctx.SaveChangesAsync();

                return RedirectToAction("Default");
            }

            SetupViewbag();
            ViewBag.Qualifications = await _applicationService.GetQualifications(_ctx);

            return View(qualification);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Continue()
        {
            // Check the user has supplied us with some qualifications
            List<Qualification> qualifications = await _applicationService.GetQualifications(_ctx);
            if (qualifications != null && qualifications.Any())
            {
                return RedirectToAction("Manage", "Applications");
            }

            // No quals provided, inform them they must provide us with something.
            ModelState.AddModelError("NoQuals", "Please enter your qualifications or select 'I dont have any qualifications'.");
            return RedirectToAction("Create");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int id)
        {
            Qualification qualification = await _ctx.Qualifications.FindAsync(id);
            
            // User must be the owner of the qualification to delete it.
            if (qualification != null && qualification.Application.User.UserName == User.Identity.Name)
            {
                _ctx.Qualifications.Remove(qualification);
                await _ctx.SaveChangesAsync();
            }

            return RedirectToAction("Default");
        }

        protected override void SetupViewbag()
        {
            ViewBag.QualificationTypes = _ctx.QualificationTypes.ToList();
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
