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

namespace StudentPortal.Controllers
{
    public class ProspectusController : BaseApplicationController
    {
        public ProspectusController() : base()
        {
            // ...
        }

        public async Task<ActionResult> Default(int? subject)
        {
            List<Course> courses = await _ctx.Courses
                .Where(c => 
                    c.Active && // Course is active
                    (c.ActiveTo == null || c.ActiveTo.Value > DateTime.Today) && // Deadline hasn't passed (course inactive)
                    (!subject.HasValue || c.Type == subject.Value) // Matches subject area
                )
                .OrderBy(c => c.Title)
                .ThenBy(c => c.Type)
                .ToListAsync();

            ViewBag.CourseGroups = await _ctx.CourseGroups.ToListAsync();

            return View(courses);
        }

        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Course course = await _ctx.Courses.FindAsync(id);
            if (course == null)
            {
                return HttpNotFound();
            }
            return View(course);
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
