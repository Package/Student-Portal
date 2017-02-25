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
    public class EthnicityManagementController : DataController
    {


        // GET: Admin/EthnicityManagement
        public async Task<ActionResult> Default()
        {
            return View(await _ctx.Ethnicity.ToListAsync());
        }

        // GET: Admin/EthnicityManagement/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ethnicity ethnicity = await _ctx.Ethnicity.FindAsync(id);
            if (ethnicity == null)
            {
                return HttpNotFound();
            }
            return View(ethnicity);
        }

        // GET: Admin/EthnicityManagement/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/EthnicityManagement/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Ethnicity ethnicity)
        {
            if (ModelState.IsValid)
            {
                _ctx.Ethnicity.Add(ethnicity);
                await _ctx.SaveChangesAsync();
                return RedirectToAction("Default");
            }

            return View(ethnicity);
        }

        // GET: Admin/EthnicityManagement/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ethnicity ethnicity = await _ctx.Ethnicity.FindAsync(id);
            if (ethnicity == null)
            {
                return HttpNotFound();
            }
            return View(ethnicity);
        }

        // POST: Admin/EthnicityManagement/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(Ethnicity ethnicity)
        {
            if (ModelState.IsValid)
            {
                _ctx.Entry(ethnicity).State = EntityState.Modified;
                await _ctx.SaveChangesAsync();
                return RedirectToAction("Default");
            }
            return View(ethnicity);
        }

        // GET: Admin/EthnicityManagement/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ethnicity ethnicity = await _ctx.Ethnicity.FindAsync(id);
            if (ethnicity == null)
            {
                return HttpNotFound();
            }
            return View(ethnicity);
        }

        // POST: Admin/EthnicityManagement/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Ethnicity ethnicity = await _ctx.Ethnicity.FindAsync(id);
            _ctx.Ethnicity.Remove(ethnicity);
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
