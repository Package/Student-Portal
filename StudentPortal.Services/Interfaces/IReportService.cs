using StudentPortal.Domain.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StudentPortal.Domain.Models.Reports;
using StudentPortal.Domain.Models;

namespace StudentPortal.Services.Interfaces
{
    public interface IReportService
    {
        Task<List<ApplicantDetails>> IncompleteApplications(StudentPortalContext _ctx);

        Task<List<ApplicantDetails>> ApplicationsForCourse(string courseCode, string courseGroupCode, StudentPortalContext _ctx);

        IEnumerable<ApplicantDetails> ApplicationsByAge(string startAge, string endAge, StudentPortalContext _ctx);

        Dictionary<Ethnicity, int> ApplicationsByEthnicity(StudentPortalContext _ctx);

        Dictionary<Gender, int> ApplicationsByGender(StudentPortalContext _ctx);

        Dictionary<School, int> ApplicationsBySchool(StudentPortalContext _ctx);

        Dictionary<Course, int> ApplicationsByCourse(StudentPortalContext _ctx);

        Dictionary<CourseGroup, int> ApplicationsByCourseGroup(StudentPortalContext _ctx);

        Dictionary<Disability, int> ApplicationsByDisability(StudentPortalContext _ctx);

        Dictionary<ApplicationProgress, int> ApplicationsByStatus(StudentPortalContext _ctx);
    }
}
