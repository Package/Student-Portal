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
using System.Data.Entity.Validation;
using StudentPortal.Services.Interfaces;

namespace StudentPortal.Controllers
{
    [Authorize]
    public class LearningSupportController : BaseApplicationController
    {
        private readonly IApplicationService _applicationService;
        private readonly ILearningSupportService _learningSupportService;
        private readonly IDisabilityService _disabilityService;

        public LearningSupportController(
            IApplicationService _applicationService,
            ILearningSupportService _learningSupportService,
            IDisabilityService _disabilityService) : base()
        {
            this._applicationService = _applicationService;
            this._learningSupportService = _learningSupportService;
            this._disabilityService = _disabilityService;
        }

        public async Task<ActionResult> Default()
        {
            Application application = await _applicationService.GetCurrentApplication(_ctx);

            LearningSupport learningSupport = await _applicationService.GetLearningSupport(_ctx) 
                ?? new LearningSupport();

            learningSupport.Disability = await _disabilityService.SelectDisabilities(application, _ctx);

            return View(learningSupport);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Default(LearningSupport learningSupport)
        {
            Application application = await _applicationService.GetCurrentApplication(_ctx);
     
            await _disabilityService.SaveDisabilities(application, learningSupport.Disability, _ctx);

            await _learningSupportService.SaveLearningSupport(learningSupport, application, _ctx);

            return RedirectToAction("Manage", "Applications");
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
