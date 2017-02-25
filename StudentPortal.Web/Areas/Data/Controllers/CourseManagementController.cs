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

namespace StudentPortal.Areas.Data.Controllers
{
    public class CourseManagementController : DataController
    {
        //private readonly StudentPortalContext _ctx = new StudentPortalContext();

        // GET: Admin/CourseManagement
        public async Task<ActionResult> Default()
        {
            return View(await _ctx.Courses.ToListAsync());
        }

        // GET: Admin/CourseManagement/Details/5
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

        // GET: Admin/CourseManagement/Create
        public ActionResult Create()
        {
            ViewBag.CourseGroups = _ctx.CourseGroups.ToList();
            return View();
        }

        // POST: Admin/CourseManagement/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Course course)
        {
            if (ModelState.IsValid)
            {
                _ctx.Courses.Add(course);
                await _ctx.SaveChangesAsync();
                return RedirectToAction("Default");
            }

            ViewBag.CourseGroups = _ctx.CourseGroups.ToList();
            return View(course);
        }

        // GET: Admin/CourseManagement/Edit/5
        public async Task<ActionResult> Edit(int? id)
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

            ViewBag.CourseGroups = _ctx.CourseGroups.ToList();
            return View(course);
        }

        // POST: Admin/CourseManagement/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(Course course)
        {
            if (ModelState.IsValid)
            {
                _ctx.Entry(course).State = EntityState.Modified;
                await _ctx.SaveChangesAsync();
                return RedirectToAction("Default");
            }

            ViewBag.CourseGroups = _ctx.CourseGroups.ToList();
            return View(course);
        }

        // GET: Admin/CourseManagement/Delete/5
        public async Task<ActionResult> Delete(int? id)
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

        // POST: Admin/CourseManagement/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Course course = await _ctx.Courses.FindAsync(id);
            _ctx.Courses.Remove(course);
            await _ctx.SaveChangesAsync();
            return RedirectToAction("Default");
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
