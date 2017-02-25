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
    public class CourseGroupManagementController : DataController
    {
        //private readonly StudentPortalContext _ctx = new StudentPortalContext();

        // GET: Admin/CourseGroupManagement
        public async Task<ActionResult> Default()
        {
            return View(await _ctx.CourseGroups.ToListAsync());
        }

        // GET: Admin/CourseGroupManagement/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CourseGroup courseGroup = await _ctx.CourseGroups.FindAsync(id);
            if (courseGroup == null)
            {
                return HttpNotFound();
            }
            return View(courseGroup);
        }

        // GET: Admin/CourseGroupManagement/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/CourseGroupManagement/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(CourseGroup courseGroup)
        {
            if (ModelState.IsValid)
            {
                _ctx.CourseGroups.Add(courseGroup);
                await _ctx.SaveChangesAsync();
                return RedirectToAction("Default");
            }

            return View(courseGroup);
        }

        // GET: Admin/CourseGroupManagement/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CourseGroup courseGroup = await _ctx.CourseGroups.FindAsync(id);
            if (courseGroup == null)
            {
                return HttpNotFound();
            }
            return View(courseGroup);
        }

        // POST: Admin/CourseGroupManagement/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(CourseGroup courseGroup)
        {
            if (ModelState.IsValid)
            {
                _ctx.Entry(courseGroup).State = EntityState.Modified;
                await _ctx.SaveChangesAsync();
                return RedirectToAction("Default");
            }
            return View(courseGroup);
        }

        // GET: Admin/CourseGroupManagement/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CourseGroup courseGroup = await _ctx.CourseGroups.FindAsync(id);
            if (courseGroup == null)
            {
                return HttpNotFound();
            }
            return View(courseGroup);
        }

        // POST: Admin/CourseGroupManagement/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            CourseGroup courseGroup = await _ctx.CourseGroups.FindAsync(id);
            _ctx.CourseGroups.Remove(courseGroup);
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
