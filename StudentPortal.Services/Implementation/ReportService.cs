using StudentPortal.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Text;
using System.Threading.Tasks;
using StudentPortal.Domain.Context;
using StudentPortal.Domain.Models.Reports;
using StudentPortal.Domain.Models;

namespace StudentPortal.Services.Implementation
{
    public class ReportService : IReportService
    {
        /// <summary>
        /// List of applications made for a particular course or course group.
        /// </summary>
        /// <param name="courseCode"></param>
        /// <param name="_ctx"></param>
        /// <returns></returns>
        public async Task<List<ApplicantDetails>> ApplicationsForCourse(string courseCode, string courseGroupCode, StudentPortalContext _ctx)
        {
            List<ApplicantDetails> applications = await (
                from a in _ctx.Applications
                join pd in _ctx.PersonalDetails on a.Id equals pd.Application.Id
                join g in _ctx.Genders on pd.Gender equals g.Id.ToString()
                join cs in _ctx.CourseSelections on a.Id equals cs.Application.Id
                join c in _ctx.Courses on cs.FirstChoice equals c.Id
                join cg in _ctx.CourseGroups on c.Type equals cg.Id
                where a.Submitted && // Only complete applications
                      (
                        ( c.Code == courseCode ||  c.Id.ToString() == courseCode ) || // For the provided course
                        ( cg.Id.ToString() == courseGroupCode) // For the provided course group
                      )
                orderby a.Finished descending
                select new ApplicantDetails
                {
                    Id = a.Id,
                    Started = a.Started,
                    Finished = a.Finished.Value,
                    Gender = g.Name,
                    Forename = pd.Forename,
                    Surname = pd.Surname,
                    Email = pd.EmailAddress,
                    DateOfBirth = pd.DateOfBirth,
                    MobileNumber = pd.MobileNumber,
                    TelephoneNumber = pd.TelephoneNumber,
                }
            ).ToListAsync();

            return applications;
        }

        /// <summary>
        /// List of applicant details for students who's age falls between a provided range, e.g 16 to 20.
        /// </summary>
        /// <param name="startAge"></param>
        /// <param name="endAge"></param>
        /// <param name="_ctx"></param>
        /// <returns></returns>
        public IEnumerable<ApplicantDetails> ApplicationsByAge(string startAge, string endAge, StudentPortalContext _ctx)
        {
            // If no ages are passed, lookup applicants born today - e.g show no results.
            DateTime startDate = DateTime.Today;
            DateTime endDate = DateTime.Today;

            if (!string.IsNullOrEmpty(startAge))
            {
                startDate = DateTime.Today.AddYears(-Convert.ToInt32(startAge));
            }
            if (!string.IsNullOrEmpty(endAge))
            {
                endDate = DateTime.Today.AddYears(-Convert.ToInt32(endAge));
            }

            List<ApplicantDetails> applications = (
                from a in _ctx.Applications
                join pd in _ctx.PersonalDetails on a.Id equals pd.Application.Id
                join g in _ctx.Genders on pd.Gender equals g.Id.ToString()
                where a.Submitted // Only complete applications
                orderby a.Finished descending
                select new ApplicantDetails
                {
                    Id = a.Id,
                    Started = a.Started,
                    Finished = a.Finished.Value,
                    Gender = g.Name,
                    Forename = pd.Forename,
                    Surname = pd.Surname,
                    Email = pd.EmailAddress,
                    DateOfBirth = pd.DateOfBirth,
                    MobileNumber = pd.MobileNumber,
                    TelephoneNumber = pd.TelephoneNumber,
                }
            ).ToList();


            foreach (ApplicantDetails applicant in applications)
            {
                DateTime dob = DateTime.Parse(applicant.DateOfBirth);

                // Born within the date range
                if (dob >= endDate && dob <= startDate)
                {
                    yield return applicant;
                }
            }
        }

        /// <summary>
        /// Breakdown of the number of completed applications by the available ethnicities.
        /// </summary>
        /// <param name="_ctx"></param>
        /// <returns></returns>
        public Dictionary<Ethnicity, int> ApplicationsByEthnicity(StudentPortalContext _ctx)
        {
            Dictionary<Ethnicity, int> mapping = new Dictionary<Ethnicity, int>();

            foreach (Ethnicity ethnicity in _ctx.Ethnicity.ToList())
            {
                int count = (
                    from a in _ctx.Applications
                    join pd in _ctx.PersonalDetails on a.Id equals pd.Application.Id
                    join e in _ctx.Ethnicity on pd.Ethnicity equals e.Id.ToString()
                    where a.Submitted && ethnicity.Id == e.Id
                    select e.Name
                ).Count();

                mapping.Add(ethnicity, count);
            }

            return mapping;
        }

        /// <summary>
        /// Breakdown of the number of completed applications by the available genders.
        /// </summary>
        /// <param name="_ctx"></param>
        /// <returns></returns>
        public Dictionary<Gender, int> ApplicationsByGender(StudentPortalContext _ctx)
        {
            Dictionary<Gender, int> mapping = new Dictionary<Gender, int>();

            foreach (Gender gender in _ctx.Genders.ToList())
            {
                int count = (
                    from a in _ctx.Applications
                    join pd in _ctx.PersonalDetails on a.Id equals pd.Application.Id
                    join g in _ctx.Genders on pd.Gender equals g.Id.ToString()
                    where a.Submitted && pd.Gender == gender.Id.ToString()
                    select g.Name
                ).Count();

                mapping.Add(gender, count);
            }

            return mapping;
        }

        /// <summary>
        /// Breakdown of the number of completed applications by the available schools.
        /// </summary>
        /// <param name="_ctx"></param>
        /// <returns></returns>
        public Dictionary<School, int> ApplicationsBySchool(StudentPortalContext _ctx)
        {
            Dictionary<School, int> mapping = new Dictionary<School, int>();

            foreach (School school in _ctx.Schools.ToList())
            {
                int count = (
                    from a in _ctx.Applications
                    join pd in _ctx.PersonalDetails on a.Id equals pd.Application.Id
                    join eh in _ctx.Education on a.Id equals eh.Application.Id
                    where a.Submitted && eh.SchoolOrCollege == school.Id.ToString()
                    select eh.SchoolOrCollege
                ).Count();

                mapping.Add(school, count);
            }

            return mapping;
        }


        /// <summary>
        /// Extract data from the database of applicants who have not finished their full application form yet.
        /// </summary>
        /// <param name="_ctx"></param>
        /// <returns></returns>
        public async Task<List<ApplicantDetails>> IncompleteApplications(StudentPortalContext _ctx)
        {
            List<ApplicantDetails> applications = await (
                from a in _ctx.Applications
                join pd in _ctx.PersonalDetails on a.Id equals pd.Application.Id
                join g in _ctx.Genders on pd.Gender equals g.Id.ToString()
                where !a.Submitted && !a.Complete
                orderby a.Started descending
                select new ApplicantDetails
                {
                    Id = a.Id,
                    Started = a.Started,
                    Gender = g.Name,
                    Forename = pd.Forename,
                    Surname = pd.Surname,
                    Email = pd.EmailAddress,
                    DateOfBirth = pd.DateOfBirth,
                    MobileNumber = pd.MobileNumber,
                    TelephoneNumber = pd.TelephoneNumber,
                }
            ).ToListAsync();

            return applications;
        }

        /// <summary>
        /// Breakdown of application numbers by status code
        /// </summary>
        /// <param name="_ctx"></param>
        /// <returns></returns>
        public Dictionary<ApplicationProgress, int> ApplicationsByStatus(StudentPortalContext _ctx)
        {
            Dictionary<ApplicationProgress, int> mapping = new Dictionary<ApplicationProgress, int>();

            foreach (ApplicationProgress progess in _ctx.ApplicationProgress.ToList())
            {
                int count = (
                    from a in _ctx.Applications
                    where a.Submitted && a.Progress == progess.Id
                    select a.Progress
                ).Count();

                mapping.Add(progess, count);
            }

            return mapping;
        }

        /// <summary>
        /// Breakdown of application numbers by course
        /// </summary>
        /// <param name="_ctx"></param>
        /// <returns></returns>
        public Dictionary<Course, int> ApplicationsByCourse(StudentPortalContext _ctx)
        {
            Dictionary<Course, int> mapping = new Dictionary<Course, int>();

            foreach (Course course in _ctx.Courses.ToList())
            {
                int count = (
                    from a in _ctx.Applications
                    join cs in _ctx.CourseSelections on a.Id equals cs.Application.Id
                    join c  in _ctx.Courses on cs.FirstChoice equals c.Id
                    where a.Submitted && c.Id == course.Id
                    select c.Id
                ).Count();

                mapping.Add(course, count);
            }

            return mapping;
        }

        /// <summary>
        /// Breakdown of application numbers by disabilities identified
        /// </summary>
        /// <param name="_ctx"></param>
        /// <returns></returns>
        public Dictionary<Disability, int> ApplicationsByDisability(StudentPortalContext _ctx)
        {
            Dictionary<Disability, int> mapping = new Dictionary<Disability, int>();

            foreach (Disability disability in _ctx.Disabilities.ToList())
            {
                int count = (
                    from a in _ctx.Applications
                    join pd in _ctx.PersonalDetails on a.Id equals pd.Application.Id
                    join ud in _ctx.UserDisabilities on a.Id equals ud.Application.Id
                    join d in _ctx.Disabilities on ud.Disability equals d.Id
                    where a.Submitted && d.Id == disability.Id
                    select d.Id
                ).Count();

                mapping.Add(disability, count);
            }

            return mapping;
        }

        /// <summary>
        /// Breakdown of applications by course group offering.
        /// </summary>
        /// <param name="_ctx"></param>
        /// <returns></returns>
        public Dictionary<CourseGroup, int> ApplicationsByCourseGroup(StudentPortalContext _ctx)
        {
            Dictionary<CourseGroup, int> mapping = new Dictionary<CourseGroup, int>();

            foreach (CourseGroup courseGroup in _ctx.CourseGroups.ToList())
            {
                int count = (
                    from a in _ctx.Applications
                    join cs in _ctx.CourseSelections on a.Id equals cs.Application.Id
                    join c in _ctx.Courses on cs.FirstChoice equals c.Id
                    where a.Submitted && c.Type == courseGroup.Id
                    select c.Id
                ).Count();

                mapping.Add(courseGroup, count);
            }

            return mapping;
        }
    }
}
