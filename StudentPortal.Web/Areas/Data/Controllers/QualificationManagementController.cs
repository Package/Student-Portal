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
    public class QualificationManagementController : DataController
    {
        //private readonly StudentPortalContext _ctx = new StudentPortalContext();

        // GET: Admin/QualificationManagement
        public async Task<ActionResult> Default()
        {
            return View(await _ctx.QualificationTypes.ToListAsync());
        }

        // GET: Admin/QualificationManagement/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            QualificationType qualificationType = await _ctx.QualificationTypes.FindAsync(id);
            if (qualificationType == null)
            {
                return HttpNotFound();
            }
            return View(qualificationType);
        }

        // GET: Admin/QualificationManagement/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/QualificationManagement/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(QualificationType qualificationType)
        {
            if (ModelState.IsValid)
            {
                _ctx.QualificationTypes.Add(qualificationType);
                await _ctx.SaveChangesAsync();
                return RedirectToAction("Default");
            }

            return View(qualificationType);
        }

        // GET: Admin/QualificationManagement/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            QualificationType qualificationType = await _ctx.QualificationTypes.FindAsync(id);
            if (qualificationType == null)
            {
                return HttpNotFound();
            }
            return View(qualificationType);
        }

        // POST: Admin/QualificationManagement/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(QualificationType qualificationType)
        {
            if (ModelState.IsValid)
            {
                _ctx.Entry(qualificationType).State = EntityState.Modified;
                await _ctx.SaveChangesAsync();
                return RedirectToAction("Default");
            }
            return View(qualificationType);
        }

        // GET: Admin/QualificationManagement/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            QualificationType qualificationType = await _ctx.QualificationTypes.FindAsync(id);
            if (qualificationType == null)
            {
                return HttpNotFound();
            }
            return View(qualificationType);
        }

        // POST: Admin/QualificationManagement/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            QualificationType qualificationType = await _ctx.QualificationTypes.FindAsync(id);
            _ctx.QualificationTypes.Remove(qualificationType);
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
