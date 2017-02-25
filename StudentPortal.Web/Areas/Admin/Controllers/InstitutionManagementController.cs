using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using StudentPortal.Domain.Context;
using StudentPortal.Domain.Models;
using System.IO;

namespace StudentPortal.Areas.Admin.Controllers
{
    public class InstitutionManagementController : AdminController
    {
        // GET: Admin/InstitutionManagement/Create
        public async Task<ActionResult> Default()
        {
            Institution institution = await _ctx.Institutions.FirstOrDefaultAsync()
                ?? new Institution();

            return View(institution);
        }

        // POST: Admin/InstitutionManagement/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Default(Institution institution, HttpPostedFileBase logo)
        {
            if (ModelState.IsValid)
            {
                // Handle uploading the logo
                if (Request.Files.Count > 0)
                {
                    var file = Request.Files[0];

                    if (file != null && file.ContentLength > 0)
                    {
                        string path = Path.Combine(Server.MapPath("~/Images/"), "logo.png");
                        file.SaveAs(path);
                        institution.Logo = "logo.png";
                    }
                }

                _ctx.Institutions.AddOrUpdate(i => i.Id, institution);
                await _ctx.SaveChangesAsync();
                return RedirectToAction("Default");
            }

            return View(institution);
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
