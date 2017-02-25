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
using StudentPortal.Services.Interfaces;

namespace StudentPortal.Controllers
{
    [Authorize]
    public class AddressController : BaseApplicationController
    {
        private readonly IApplicationService _applicationService;

        public AddressController(IApplicationService _applicationService) : base()
        {
            this._applicationService = _applicationService;
        }

        public async Task<ActionResult> Default()
        {
            Address address = await _applicationService.GetAddress(_ctx) 
                ?? new Address();

            return View(address);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Default(Address address)
        {
            Application application = await _applicationService.GetCurrentApplication(_ctx);

            if (ModelState.IsValid)
            {
                address.Application = application;

                Address currentAddress = await _applicationService.GetAddress(_ctx);
                if (currentAddress != null)
                {
                    address.Id = currentAddress.Id;
                }

                _ctx.Addressses.AddOrUpdate(a => a.Id, address);
                await _ctx.SaveChangesAsync();

                return RedirectToAction("Manage", "Applications");
            }

            return View(address);
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
