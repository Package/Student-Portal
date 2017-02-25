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
    public class ApplicationProgressManagementController : DataController
    {
        public async Task<ActionResult> Default()
        {
            return View(await _ctx.ApplicationProgress.ToListAsync());
        }

        // GET: Data/ApplicationProgressManagement/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ApplicationProgress applicationProgress = await _ctx.ApplicationProgress.FindAsync(id);
            if (applicationProgress == null)
            {
                return HttpNotFound();
            }
            return View(applicationProgress);
        }

        // GET: Data/ApplicationProgressManagement/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Data/ApplicationProgressManagement/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(ApplicationProgress applicationProgress)
        {
            if (ModelState.IsValid)
            {
                _ctx.ApplicationProgress.Add(applicationProgress);
                await _ctx.SaveChangesAsync();
                return RedirectToAction("Default");
            }

            return View(applicationProgress);
        }

        // GET: Data/ApplicationProgressManagement/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ApplicationProgress applicationProgress = await _ctx.ApplicationProgress.FindAsync(id);
            if (applicationProgress == null)
            {
                return HttpNotFound();
            }
            return View(applicationProgress);
        }

        // POST: Data/ApplicationProgressManagement/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(ApplicationProgress applicationProgress)
        {
            if (ModelState.IsValid)
            {
                _ctx.Entry(applicationProgress).State = EntityState.Modified;
                await _ctx.SaveChangesAsync();
                return RedirectToAction("Default");
            }
            return View(applicationProgress);
        }

        // GET: Data/ApplicationProgressManagement/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ApplicationProgress applicationProgress = await _ctx.ApplicationProgress.FindAsync(id);
            if (applicationProgress == null)
            {
                return HttpNotFound();
            }
            return View(applicationProgress);
        }

        // POST: Data/ApplicationProgressManagement/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            ApplicationProgress applicationProgress = await _ctx.ApplicationProgress.FindAsync(id);
            _ctx.ApplicationProgress.Remove(applicationProgress);
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
