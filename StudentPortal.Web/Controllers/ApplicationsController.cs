using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using StudentPortal.Services.Interfaces;
using StudentPortal.Domain.Context;
using StudentPortal.Domain.Models;
using StudentPortal.ViewModels;

namespace StudentPortal.Controllers
{
    [Authorize]
    public class ApplicationsController : BaseApplicationController
    {
        private readonly IUserService _userService;
        private readonly IApplicationService _applicationService;
        private readonly IMailService _mailService;

        public ApplicationsController(
            IUserService _userService, 
            IApplicationService _applicationService,
            IMailService _mailService) : base()
        {
            this._userService = _userService;
            this._applicationService = _applicationService;
            this._mailService = _mailService;
        }

        public async Task<ActionResult> Default()
        {
            List<Application> applications = await _applicationService.GetUsersApplications(User.Identity.Name, _ctx);
            return View(applications);
        }

        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Application application = await _ctx.Applications.FindAsync(id);
            if (application == null)
            {
                return HttpNotFound();
            }
            return View(application);
        }

        public ActionResult Create(string course)
        {
            // If the user has selected a specfic course to apply for, cache it until they
            // get to the course selection screen, when we can pre-populate their courses.
            if (!string.IsNullOrEmpty(course))
            {
                Session["CourseId"] = course;
            }

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Begin()
        {
            // Setup and begin the new application
            Application application = new Application
            {
                Complete = false,
                Submitted = false,
                Started = DateTime.Now,
                State = "In progress",
                User = await _userService.GetUser(User.Identity.Name, _ctx)
            };

            _ctx.Applications.Add(application);
            await _ctx.SaveChangesAsync();

            // Store current application ID for use in the application form.
            Session["ApplicationId"] = application.Id;

            return RedirectToAction("Default", "Personal");
        }

        public async Task<ActionResult> Continue(int id)
        {
            await _applicationService.OwnsApplication(id, User.Identity.Name, _ctx, forceRedirect: true);

            Session["ApplicationId"] = id;
            ApplicationDetailsViewModel model = await _applicationService.GetApplicationDetails(_ctx, id);

            // Don't allow you to continue an application that has already been submitted.
            if (model.Application.Submitted)
            {
                Session["ApplicationId"] = null;
                return Redirect("~/");
            }

            return RedirectToAction("Manage");
        }

        public async Task<ActionResult> Manage()
        {
            Application application = await _applicationService.GetCurrentApplication(_ctx);

            await _applicationService.OwnsApplication(application.Id, User.Identity.Name, _ctx, forceRedirect: true);

            ApplicationDetailsViewModel model = await _applicationService.GetApplicationDetails(_ctx, application.Id);

            // Mark the application as complete if all sections have been filled out.
            if (!application.Complete && model.Complete)
            {
                application.Complete = true;
                await _ctx.SaveChangesAsync();
            }

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Submit()
        {
            Application application = await _applicationService.GetCurrentApplication(_ctx);

            // Ensure user owns the application - not trying to submit someone elses
            await _applicationService.OwnsApplication(application.Id, User.Identity.Name, _ctx, forceRedirect: true);

            // Ensure the application has been completed
            if (application.Complete)
            {
                // Mark the application and completed
                await _applicationService.CompleteApplication(application, _ctx);

                // Gather application details to be included in the confirmation email
                ApplicationDetailsViewModel model = await _applicationService.GetApplicationDetails(_ctx, application.Id);
                Dictionary<string, string> parameters = new Dictionary<string, string> {
                    { "Name", model.PersonalDetails.Forename },
                    { "ContactEmail", ViewBag.ContactEmail },
                    { "ContactPhone",  ViewBag.ContactPhone },
                    { "Institution",  ViewBag.Institution }
                };

                // Send a confirmation email to the user
                await _mailService.SendEmailAsync(
                    emailAddress: model.PersonalDetails.EmailAddress,
                    subject: "Thank you for your application",
                    message: null,
                    emailTemplate: "ApplicationComplete",
                    parameters: parameters
                );

                Session["ApplicationId"] = null;

                return RedirectToAction("Thanks", "Home");
            }

            return Redirect("~/");
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
