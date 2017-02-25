using StudentPortal.Domain.Context;
using StudentPortal.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentPortal.Services.Interfaces
{
    public interface IDisabilityService
    {
        Task<string> ConvertDisabilitiesToString(Application application, StudentPortalContext _ctx);

        Task<List<UserDisability>> GetDisabilities(Application application, StudentPortalContext _ctx);

        Task<List<Disability>> SelectDisabilities(Application application, StudentPortalContext _ctx);

        Task RemoveOldDisabilities(Application application, StudentPortalContext _ctx);

        Task SaveDisabilities(Application application, List<Disability> disabilities, StudentPortalContext _ctx);
    }
}
