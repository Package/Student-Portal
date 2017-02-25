using StudentPortal.Domain.Context;
using StudentPortal.Domain.Models;
using StudentPortal.Domain.Models.Reports;
using StudentPortal.Services.Interfaces;
using System;
using System.Data.Entity;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace StudentPortal.Areas.Reporting.Controllers
{
    public class ApplicationReportsController : ReportingController
    {
        private readonly IReportService _reportService;

        public ApplicationReportsController(IReportService _reportService)
        {
            this._reportService = _reportService;
        }

        public async Task<ActionResult> IncompleteApplications()
        {
            List<ApplicantDetails> applications = await _reportService.IncompleteApplications(_ctx);

            return View(applications);
        }

        public ActionResult ApplicationsByEthnicity()
        {
            Dictionary<Ethnicity, int> mapping = _reportService.ApplicationsByEthnicity(_ctx);

            return View(mapping);
        }

        public ActionResult ApplicationsBySchool()
        {
            Dictionary<School, int> mapping = _reportService.ApplicationsBySchool(_ctx);

            return View(mapping);
        }

        public ActionResult ApplicationsByGender()
        {
            Dictionary<Gender, int> mapping = _reportService.ApplicationsByGender(_ctx);

            return View(mapping);
        }

        public ActionResult ApplicationsByStatus()
        {
            Dictionary<ApplicationProgress, int> mapping = _reportService.ApplicationsByStatus(_ctx);

            return View(mapping);
        }

        public ActionResult ApplicationsByCourse()
        {
            Dictionary<Course, int> mapping = _reportService.ApplicationsByCourse(_ctx);

            return View(mapping);
        }

        public ActionResult ApplicationsByCourseGroup()
        {
            Dictionary<CourseGroup, int> mapping = _reportService.ApplicationsByCourseGroup(_ctx);

            return View(mapping);
        }

        public ActionResult ApplicationsByDisability()
        {
            Dictionary<Disability, int> mapping = _reportService.ApplicationsByDisability(_ctx);

            return View(mapping);
        }

        public ActionResult ApplicationsByAge(string startAge, string endAge)
        {
            List<ApplicantDetails> applications = _reportService.ApplicationsByAge(startAge, endAge, _ctx).ToList();

            ViewBag.StartAge = startAge;
            ViewBag.EndAge = endAge;

            return View(applications);
        }

        public async Task<ActionResult> ApplicationsForCourse(string courseCode, string courseGroupCode)
        {
            List<ApplicantDetails> applications = await _reportService.ApplicationsForCourse(courseCode, courseGroupCode, _ctx);

            ViewBag.Courses = await _ctx.Courses.ToListAsync();
            ViewBag.CourseGroups = await _ctx.CourseGroups.ToListAsync();
            ViewBag.CourseCode = courseCode;
            ViewBag.CourseGroupCode = courseGroupCode;

            return View(applications);
        }
    }
}