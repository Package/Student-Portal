using StudentPortal.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;
using System.Threading.Tasks;
using StudentPortal.Domain.Context;
using StudentPortal.Domain.Models;
using System.Web;
using System.Web.SessionState;
using StudentPortal.ViewModels;

namespace StudentPortal.Services.Implementation
{
    public class ApplicationService : IApplicationService
    {
        private HttpResponse Response { get { return HttpContext.Current.Response; } }

        private HttpSessionState Session { get { return HttpContext.Current.Session; } }

        private readonly IDisabilityService _disabilityService;

        public ApplicationService(IDisabilityService _disabilityService)
        {
            this._disabilityService = _disabilityService;
        }

        /// <summary>
        /// Handle updating an application's details once it has been submitted.
        /// </summary>
        /// <param name="application"></param>
        /// <param name="_ctx"></param>
        /// <returns></returns>
        public async Task CompleteApplication(Application application, StudentPortalContext _ctx)
        {
            application.Submitted = true;
            application.Complete = true;
            application.Finished = DateTime.Now;
            application.State = "Completed - awaiting review";

            await _ctx.SaveChangesAsync();
        }

        /// <summary>
        /// Get the full details of each of the stages of the application provided.
        /// If no application ID is provided, it is assumed that the we want details of the current
        /// application in progress.
        /// </summary>
        /// <param name="applicationId"></param>
        /// <param name="_ctx"></param>
        /// <returns></returns>
        public async Task<ApplicationDetailsViewModel> GetApplicationDetails(StudentPortalContext _ctx, int? applicationId = null)
        {
            Application application = await GetCurrentApplication(_ctx, applicationId);

            ApplicationDetailsViewModel model = new ApplicationDetailsViewModel
            {
                Application = application,
                PersonalDetails = await GetPersonalDetails(_ctx, applicationId),
                Address = await GetAddress(_ctx, applicationId),
                CourseSelection = await GetCourses(_ctx, applicationId),
                Qualifications = await GetQualifications(_ctx, applicationId),
                Education = await GetEducationHistory(_ctx, applicationId),
                LearningSupport = await GetLearningSupport(_ctx, applicationId),
                UserDisabilities = await _disabilityService.GetDisabilities(application, _ctx),
                DisabilityString = await _disabilityService.ConvertDisabilitiesToString(application, _ctx),
                Progress = await _ctx.ApplicationProgress.ToListAsync(),
                Titles = await _ctx.Titles.ToListAsync(),
                Genders = await _ctx.Genders.ToListAsync(),
                Countries = await _ctx.Countries.ToListAsync(),
                Ethnicities = await _ctx.Ethnicity.ToListAsync(),
                Courses = await _ctx.Courses.ToListAsync(),
                Schools = await _ctx.Schools.ToListAsync(),
                Disabilities = await _ctx.Disabilities.ToListAsync(),
            };

            return model;
        }

        /// <summary>
        /// Check if the provided user is the owner of the provided application.
        /// Optionally, send them back to the home screen if they do not own the application.
        /// </summary>
        /// <param name="applicationId"></param>
        /// <param name="userName"></param>
        /// <param name="_ctx"></param>
        /// <param name="forceRedirect"></param>
        /// <returns></returns>
        public async Task<bool> OwnsApplication(int applicationId, string userName, StudentPortalContext _ctx, bool forceRedirect = false)
        {
            // Find application and verify it exists
            Application application = await _ctx.Applications.FindAsync(applicationId);
            if (application != null)
            {
                // Check user owns application
                if (application.User.UserName == userName)
                {
                    return true;
                }
            }

            // Optionally, send back to home screen as they aren't the application owner.
            if (forceRedirect)
            {
                Response.Redirect("~/");
            }

            return false;
        }

        /// <summary>
        /// Get a list of all the applications that belong to the provided user.
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="_ctx"></param>
        /// <returns></returns>
        public async Task<List<Application>> GetUsersApplications(string userName, StudentPortalContext _ctx)
        {
            List<Application> applications = await _ctx.Applications
                .Where(a => a.User.UserName == userName)
                .OrderBy(a => a.Started)
                .ToListAsync();

            return applications;
        }

        /// <summary>
        /// Get details of the current application in progress.
        /// </summary>
        /// <param name="_ctx"></param>
        /// <returns></returns>
        public async Task<Application> GetCurrentApplication(StudentPortalContext _ctx, int? applicationId = null)
        {
            // Get the current application ID from the session.
            int appId = -1;
            if (applicationId.HasValue)
            {
                appId = applicationId.Value;
            }
            else if (Session["ApplicationId"] != null)
            {
                appId = Convert.ToInt32(Session["ApplicationId"]);
            }

            // Find details of the application.
            Application application = await _ctx.Applications.FindAsync(appId);

            // Ensure the application exists.
            if (application == null)
            {
                HttpContext.Current.Response.Redirect("~/");
            }

            return application;
        }

        /// <summary>
        /// Get the current personal details for an application.
        /// </summary>
        /// <param name="_ctx"></param>
        /// <returns></returns>
        public async Task<PersonalDetails> GetPersonalDetails(StudentPortalContext _ctx, int? applicationId = null)
        {
            Application application = await GetCurrentApplication(_ctx, applicationId);
            return await _ctx.PersonalDetails.SingleOrDefaultAsync(p => p.Application.Id == application.Id);
        }

        /// <summary>
        /// Get the current address details for an application.
        /// </summary>
        /// <param name="_ctx"></param>
        /// <returns></returns>
        public async Task<Address> GetAddress(StudentPortalContext _ctx, int? applicationId = null)
        {
            Application application = await GetCurrentApplication(_ctx, applicationId);
            return await _ctx.Addressses.SingleOrDefaultAsync(a => a.Application.Id == application.Id);
        }

        /// <summary>
        /// Get the current course selection details for an application.
        /// </summary>
        /// <param name="_ctx"></param>
        /// <returns></returns>
        public async Task<CourseSelection> GetCourses(StudentPortalContext _ctx, int? applicationId = null)
        {
            Application application = await GetCurrentApplication(_ctx, applicationId);
            return await _ctx.CourseSelections.SingleOrDefaultAsync(c => c.Application.Id == application.Id);
        }

        /// <summary>
        /// Get the current qualifications entered for an application.
        /// </summary>
        /// <param name="_ctx"></param>
        /// <returns></returns>
        public async Task<List<Qualification>> GetQualifications(StudentPortalContext _ctx, int? applicationId = null)
        {
            Application application = await GetCurrentApplication(_ctx, applicationId);
            return await _ctx.Qualifications.Where(q => q.Application.Id == application.Id).ToListAsync();

        }

        /// <summary>
        /// Get the current education history details for an application.
        /// </summary>
        /// <param name="_ctx"></param>
        /// <returns></returns>
        public async Task<EducationHistory> GetEducationHistory(StudentPortalContext _ctx, int? applicationId = null)
        {
            Application application = await GetCurrentApplication(_ctx, applicationId);
            return await _ctx.Education.SingleOrDefaultAsync(e => e.Application.Id == application.Id);
        }

        /// <summary>
        /// Get the current learning support details for an application.
        /// </summary>
        /// <param name="_ctx"></param>
        /// <returns></returns>
        public async Task<LearningSupport> GetLearningSupport(StudentPortalContext _ctx, int? applicationId = null)
        {
            Application application = await GetCurrentApplication(_ctx, applicationId);
            return await _ctx.LearningSupport.SingleOrDefaultAsync(l => l.Application.Id == application.Id);
        }
    }
}
