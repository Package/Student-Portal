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
    public class GenderManagementController : DataController
    {


        // GET: Admin/GenderManagement
        public async Task<ActionResult> Default()
        {
            return View(await _ctx.Genders.ToListAsync());
        }

        // GET: Admin/GenderManagement/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Gender gender = await _ctx.Genders.FindAsync(id);
            if (gender == null)
            {
                return HttpNotFound();
            }
            return View(gender);
        }

        // GET: Admin/GenderManagement/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/GenderManagement/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Gender gender)
        {
            if (ModelState.IsValid)
            {
                _ctx.Genders.Add(gender);
                await _ctx.SaveChangesAsync();
                return RedirectToAction("Default");
            }

            return View(gender);
        }

        // GET: Admin/GenderManagement/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Gender gender = await _ctx.Genders.FindAsync(id);
            if (gender == null)
            {
                return HttpNotFound();
            }
            return View(gender);
        }

        // POST: Admin/GenderManagement/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(Gender gender)
        {
            if (ModelState.IsValid)
            {
                _ctx.Entry(gender).State = EntityState.Modified;
                await _ctx.SaveChangesAsync();
                return RedirectToAction("Default");
            }
            return View(gender);
        }

        // GET: Admin/GenderManagement/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Gender gender = await _ctx.Genders.FindAsync(id);
            if (gender == null)
            {
                return HttpNotFound();
            }
            return View(gender);
        }

        // POST: Admin/GenderManagement/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Gender gender = await _ctx.Genders.FindAsync(id);
            _ctx.Genders.Remove(gender);
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
