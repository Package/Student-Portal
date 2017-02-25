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
    public class TitleManagementController : DataController
    {

        // GET: Admin/TitleManagement
        public async Task<ActionResult> Default()
        {
            return View(await _ctx.Titles.ToListAsync());
        }

        // GET: Admin/TitleManagement/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Title title = await _ctx.Titles.FindAsync(id);
            if (title == null)
            {
                return HttpNotFound();
            }
            return View(title);
        }

        // GET: Admin/TitleManagement/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/TitleManagement/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Title title)
        {
            if (ModelState.IsValid)
            {
                _ctx.Titles.Add(title);
                await _ctx.SaveChangesAsync();
                return RedirectToAction("Default");
            }

            return View(title);
        }

        // GET: Admin/TitleManagement/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Title title = await _ctx.Titles.FindAsync(id);
            if (title == null)
            {
                return HttpNotFound();
            }
            return View(title);
        }

        // POST: Admin/TitleManagement/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(Title title)
        {
            if (ModelState.IsValid)
            {
                _ctx.Entry(title).State = EntityState.Modified;
                await _ctx.SaveChangesAsync();
                return RedirectToAction("Default");
            }
            return View(title);
        }

        // GET: Admin/TitleManagement/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Title title = await _ctx.Titles.FindAsync(id);
            if (title == null)
            {
                return HttpNotFound();
            }
            return View(title);
        }

        // POST: Admin/TitleManagement/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Title title = await _ctx.Titles.FindAsync(id);
            _ctx.Titles.Remove(title);
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
