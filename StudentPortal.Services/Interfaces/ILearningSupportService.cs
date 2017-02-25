using StudentPortal.Domain.Context;
using StudentPortal.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentPortal.Services.Interfaces
{
    public interface ILearningSupportService
    {
        Task SaveLearningSupport(LearningSupport learningSupport, Application application, StudentPortalContext _ctx);
    }
}
