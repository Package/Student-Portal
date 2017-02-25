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
    public class CountryManagementController : DataController
    {
        // GET: Admin/CountryManagement
        public async Task<ActionResult> Default()
        {
            return View(await _ctx.Countries.ToListAsync());
        }

        // GET: Admin/CountryManagement/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Country country = await _ctx.Countries.FindAsync(id);
            if (country == null)
            {
                return HttpNotFound();
            }
            return View(country);
        }

        // GET: Admin/CountryManagement/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/CountryManagement/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Country country)
        {
            if (ModelState.IsValid)
            {
                _ctx.Countries.Add(country);
                await _ctx.SaveChangesAsync();
                return RedirectToAction("Default");
            }

            return View(country);
        }

        // GET: Admin/CountryManagement/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Country country = await _ctx.Countries.FindAsync(id);
            if (country == null)
            {
                return HttpNotFound();
            }
            return View(country);
        }

        // POST: Admin/CountryManagement/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(Country country)
        {
            if (ModelState.IsValid)
            {
                _ctx.Entry(country).State = EntityState.Modified;
                await _ctx.SaveChangesAsync();
                return RedirectToAction("Default");
            }
            return View(country);
        }

        // GET: Admin/CountryManagement/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Country country = await _ctx.Countries.FindAsync(id);
            if (country == null)
            {
                return HttpNotFound();
            }
            return View(country);
        }

        // POST: Admin/CountryManagement/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Country country = await _ctx.Countries.FindAsync(id);
            _ctx.Countries.Remove(country);
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
