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
    public class DisabilityManagementController : DataController
    {
        //private readonly StudentPortalContext _ctx = new StudentPortalContext();

        // GET: Admin/DisabilityManagement
        public async Task<ActionResult> Default()
        {
            return View(await _ctx.Disabilities.ToListAsync());
        }

        // GET: Admin/DisabilityManagement/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Disability disability = await _ctx.Disabilities.FindAsync(id);
            if (disability == null)
            {
                return HttpNotFound();
            }
            return View(disability);
        }

        // GET: Admin/DisabilityManagement/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/DisabilityManagement/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Disability disability)
        {
            if (ModelState.IsValid)
            {
                _ctx.Disabilities.Add(disability);
                await _ctx.SaveChangesAsync();
                return RedirectToAction("Default");
            }

            return View(disability);
        }

        // GET: Admin/DisabilityManagement/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Disability disability = await _ctx.Disabilities.FindAsync(id);
            if (disability == null)
            {
                return HttpNotFound();
            }
            return View(disability);
        }

        // POST: Admin/DisabilityManagement/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(Disability disability)
        {
            if (ModelState.IsValid)
            {
                _ctx.Entry(disability).State = EntityState.Modified;
                await _ctx.SaveChangesAsync();
                return RedirectToAction("Default");
            }
            return View(disability);
        }

        // GET: Admin/DisabilityManagement/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Disability disability = await _ctx.Disabilities.FindAsync(id);
            if (disability == null)
            {
                return HttpNotFound();
            }
            return View(disability);
        }

        // POST: Admin/DisabilityManagement/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Disability disability = await _ctx.Disabilities.FindAsync(id);
            _ctx.Disabilities.Remove(disability);
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
