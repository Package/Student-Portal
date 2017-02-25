using StudentPortal.Services.Interfaces;
using System;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StudentPortal.Domain.Context;
using StudentPortal.Domain.Models;

namespace StudentPortal.Services.Implementation
{
    public class LearningSupportService : ILearningSupportService
    {
        private readonly IApplicationService _applicationService;

        public LearningSupportService(IApplicationService _applicationService)
        {
            this._applicationService = _applicationService;
        }

        /// <summary>
        /// Save learning support details related to the current application.
        /// </summary>
        /// <param name="learningSupport"></param>
        /// <param name="application"></param>
        /// <param name="_ctx"></param>
        /// <returns></returns>
        public async Task SaveLearningSupport(LearningSupport learningSupport, Application application, StudentPortalContext _ctx)
        {
            learningSupport.Application = application;

            LearningSupport currentSupport = await _applicationService.GetLearningSupport(_ctx);
            if (currentSupport != null)
            {
                learningSupport.Id = currentSupport.Id;
            }

            _ctx.LearningSupport.AddOrUpdate(l => l.Id, learningSupport);
            await _ctx.SaveChangesAsync();
        }
    }
}
