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
    public class CoursesController : BaseApplicationController
    {
        private readonly IApplicationService _applicationService;

        public CoursesController(IApplicationService _applicationService) : base()
        {
            this._applicationService = _applicationService;
        }

        public async Task<ActionResult> Default(int? id)
        {
            CourseSelection courseSelection = await _applicationService.GetCourses(_ctx)
                ?? new CourseSelection();

            courseSelection = await TryPreseletCourse(courseSelection);

            SetupViewbag();

            return View(courseSelection);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Default(CourseSelection courseSelection)
        {
            Application application = await _applicationService.GetCurrentApplication(_ctx);

            if (ModelState.IsValid)
            {
                courseSelection.Application = application;

                CourseSelection currentSelection = await _applicationService.GetCourses(_ctx);
                if (currentSelection != null)
                {
                    courseSelection.Id = currentSelection.Id;
                }

                _ctx.CourseSelections.AddOrUpdate(c => c.Id, courseSelection);
                await _ctx.SaveChangesAsync();

                return RedirectToAction("Manage", "Applications");
            }

            SetupViewbag();

            return View(courseSelection);
        }

        protected override void SetupViewbag()
        {
            base.SetupViewbag();

            ViewBag.Courses = _ctx.Courses.Where(c => c.Active).ToList();
            ViewBag.CourseGroups = _ctx.CourseGroups.ToList();
        }

        private async Task<CourseSelection> TryPreseletCourse(CourseSelection courseSelection)
        {
            // Pre-select the users first choice course based on the course they chose to apply for.
            if (courseSelection.FirstChoice == 0 && Session["CourseId"] != null)
            {
                string courseCode = (string) Session["CourseId"];
                Course course = await _ctx.Courses.FirstOrDefaultAsync(c => c.Code == courseCode);
                if (course != null)
                {
                    courseSelection.FirstChoice = course.Id;
                }
            }

            return courseSelection;
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
