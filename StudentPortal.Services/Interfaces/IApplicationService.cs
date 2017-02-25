using StudentPortal.Domain.Context;
using StudentPortal.Domain.Models;
using StudentPortal.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace StudentPortal.Services.Interfaces
{
    public interface IApplicationService
    {
        Task CompleteApplication(Application application, StudentPortalContext _ctx);
        Task<ApplicationDetailsViewModel> GetApplicationDetails(StudentPortalContext _ctx, int? applicationId = null);
        Task<bool> OwnsApplication(int applicationId, string userName, StudentPortalContext _ctx, bool forceRedirect = false);
        Task<List<Application>> GetUsersApplications(string userName, StudentPortalContext _ctx);
        Task<Application> GetCurrentApplication(StudentPortalContext _ctx, int? applicationId = null);
        Task<PersonalDetails> GetPersonalDetails(StudentPortalContext _ctx, int? applicationId = null);
        Task<Address> GetAddress(StudentPortalContext _ctx, int? applicationId = null);
        Task<CourseSelection> GetCourses(StudentPortalContext _ctx, int? applicationId = null);
        Task<List<Qualification>> GetQualifications(StudentPortalContext _ctx, int? applicationId = null);
        Task<EducationHistory> GetEducationHistory(StudentPortalContext _ctx, int? applicationId = null);
        Task<LearningSupport> GetLearningSupport(StudentPortalContext _ctx, int? applicationId = null);
    }
}
