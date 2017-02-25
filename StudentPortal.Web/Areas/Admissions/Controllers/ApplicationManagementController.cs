using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using StudentPortal.Domain.Context;
using StudentPortal.Domain.Models;
using PagedList;
using PagedList.Mvc;
using StudentPortal.Services.Interfaces;
using StudentPortal.ViewModels;
using StudentPortal.Domain.ViewModels;

namespace StudentPortal.Areas.Admissions.Controllers
{
    public class ApplicationManagementController : AdmissionsController
    {
        private readonly IApplicationService _applicationService;
        private readonly IMailService _mailService;

        public ApplicationManagementController(
            IApplicationService _applicationService,
            IMailService _mailService) : base()
        {
            this._applicationService = _applicationService;
            this._mailService = _mailService;
        }

        public ActionResult Default(string Search, int? Page)
        {
            // Matches performed on lowercase search
            if (!string.IsNullOrEmpty(Search))
            {
                Search = Search.ToLower().Trim();
            }

            IPagedList<BasicApplicationDetailsViewModel> applications = _ctx.Applications
                .Join(
                    _ctx.PersonalDetails, 
                    application => application.Id,
                    details => details.Application.Id,
                    (application, details) => new BasicApplicationDetailsViewModel { Application = application, PersonalDetails = details }
                )
                .OrderByDescending(a => a.Application.Finished)
                .Where(a => 
                    a.Application.Submitted && 
                    (
                        // Perform filtering based on what was searched
                        string.IsNullOrEmpty(Search) ||
                        a.PersonalDetails.Forename.ToLower().Contains(Search) ||
                        a.PersonalDetails.Surname.ToLower().Contains(Search) ||
                        a.PersonalDetails.EmailAddress.ToLower().Contains(Search) ||
                        a.Application.Id.ToString() == Search
                    )
                )
                .ToPagedList(Page ?? 1, 25);

            return View(applications);
        }

        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            ApplicationDetailsViewModel model = await _applicationService.GetApplicationDetails(_ctx, id);

            return View(model);
        }

        public async Task<ActionResult> AdvancedSearchForm()
        {
            ViewBag.Courses = await _ctx.Courses.ToListAsync();
            ViewBag.CourseGroups = await _ctx.CourseGroups.ToListAsync();
            ViewBag.ApplicationProgress = await _ctx.ApplicationProgress.ToListAsync();

            return View(new AdvancedApplicationSearchViewModel { });
        }

        public ActionResult AdvancedSearch(string Search, string StartDate, string EndDate, int? CourseId, int? CourseGroupId, int? ProgressId, int? Page)
        {
            DateTime startDate = string.IsNullOrEmpty(StartDate) ? DateTime.Now : DateTime.Parse(StartDate);
            DateTime endDate = string.IsNullOrEmpty(EndDate) ? DateTime.Now : DateTime.Parse(EndDate);

            if (!string.IsNullOrEmpty(Search))
            {
                Search = Search.ToLower().Trim();
            }
    
            IPagedList<BasicApplicationDetailsViewModel> applications = (
                from a in _ctx.Applications
                join pd in _ctx.PersonalDetails on a.Id equals pd.Application.Id
                join cs in _ctx.CourseSelections on a.Id equals cs.Application.Id
                join c in _ctx.Courses on cs.FirstChoice equals c.Id
                join cg in _ctx.CourseGroups on c.Type equals cg.Id
                where a.Submitted &&
                    (CourseId == null || cs.FirstChoice == CourseId) && // Match course search
                    (CourseGroupId == null || cg.Id == CourseGroupId) && // Match course group
                    (ProgressId == null || a.Progress == ProgressId) && // Match application progress state
                    (string.IsNullOrEmpty(StartDate) || a.Finished >= startDate) && // Application completed after start date
                    (string.IsNullOrEmpty(EndDate) || a.Finished <= endDate) && // Application completed before finish date
                    (
                        // Perform filtering based on what was searched
                        string.IsNullOrEmpty(Search) ||
                        pd.Forename.ToLower().Contains(Search) ||
                        pd.Surname.ToLower().Contains(Search) ||
                        pd.EmailAddress.ToLower().Contains(Search) ||
                        pd.Id.ToString() == Search
                    )
                orderby a.Finished descending
                select new BasicApplicationDetailsViewModel
                {
                    Application = a,
                    PersonalDetails = pd,
                    CourseSelection = cs
                }
            ).ToPagedList(Page ?? 1, 25);

            return View(applications);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Update(int applicationId, int progressId)
        {
            Application application = await _applicationService.GetCurrentApplication(_ctx, applicationId);
            application.Progress = progressId;

            await _ctx.SaveChangesAsync();

            // Gather application details to be included in the email
            ApplicationDetailsViewModel model = await _applicationService.GetApplicationDetails(_ctx, application.Id);
            Dictionary<string, string> parameters = new Dictionary<string, string> {
                    { "Name", model.PersonalDetails.Forename },
                    { "Institution",  ViewBag.Institution }
                };

            // Send an email to the user to alert them of the change
            await _mailService.SendEmailAsync(
                emailAddress: model.PersonalDetails.EmailAddress,
                subject: "Update on your application",
                message: null,
                emailTemplate: "ApplicationUpdate",
                parameters: parameters
            );

            return RedirectToAction("Details", routeValues: new { id = applicationId });
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
