using StudentPortal.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StudentPortal.Domain.Context;
using StudentPortal.Domain.Models;

namespace StudentPortal.Services.Implementation
{
    public class DisabilityService : IDisabilityService
    {
        /// <summary>
        /// Comma seperate format each of the disabilities identified in the provided application.
        /// </summary>
        /// <param name="application"></param>
        /// <param name="_ctx"></param>
        /// <returns></returns>
        public async Task<string> ConvertDisabilitiesToString(Application application, StudentPortalContext _ctx)
        {
            List<Disability> disabilities = await SelectDisabilities(application, _ctx);

            List<string> disabilityNames = disabilities.Where(d => d.HasDisability).Select(d => d.Name).ToList();
            if (disabilityNames == null || !disabilityNames.Any())
            {
                return "None";
            }

            return disabilityNames.Aggregate((x, y) => string.Format("{0}, {1}", x, y));
        }

        /// <summary>
        /// Get a list of disabilities that have been identified in the current application.
        /// </summary>
        /// <param name="application"></param>
        /// <param name="_ctx"></param>
        /// <returns></returns>
        public async Task<List<UserDisability>> GetDisabilities(Application application, StudentPortalContext _ctx)
        {
            List<UserDisability> disabilities = await _ctx.UserDisabilities
             .Where(u => u.Application.Id == application.Id)
             .ToListAsync();

            return disabilities;
        }

        /// <summary>
        /// Remove any old disbilities tied to the current user's application.
        /// </summary>
        /// <param name="application"></param>
        /// <param name="_ctx"></param>
        /// <returns></returns>
        public async Task RemoveOldDisabilities(Application application, StudentPortalContext _ctx)
        {
            _ctx.UserDisabilities.RemoveRange(await GetDisabilities(application, _ctx));
        }

        /// <summary>
        /// Save the disabilities/support needs of the user for their application.
        /// </summary>
        /// <param name="application"></param>
        /// <param name="disabilities"></param>
        /// <param name="_ctx"></param>
        /// <returns></returns>
        public async Task SaveDisabilities(Application application, List<Disability> disabilities, StudentPortalContext _ctx)
        {
            await RemoveOldDisabilities(application, _ctx);

            foreach (var disability in disabilities)
            {
                if (disability.HasDisability)
                {
                    Disability dis = await _ctx.Disabilities
                        .FirstOrDefaultAsync(d => d.Name == disability.Name);

                    _ctx.UserDisabilities.Add(new UserDisability
                    {
                        Disability = dis.Id,
                        Application = application
                    });
                }
            }
        }

        /// <summary>
        /// Pre-select any disabilities the user has identified in their application.
        /// </summary>
        /// <param name="application"></param>
        /// <param name="_ctx"></param>
        /// <returns></returns>
        public async Task<List<Disability>> SelectDisabilities(Application application, StudentPortalContext _ctx)
        {
            // List of available disabilities
            List<Disability> disabilities = await _ctx.Disabilities.ToListAsync();

            // List of disabilities identified in the user's application
            List<UserDisability> userDisabilities = await _ctx.UserDisabilities
                .Where(u => u.Application.Id == application.Id)
                .ToListAsync();

            // Flag any disabilities that the user has identified
            foreach (var disability in userDisabilities)
            {
                Disability dis = disabilities.FirstOrDefault(d => d.Id == disability.Disability);
                if (dis != null)
                {
                    dis.HasDisability = true;
                }
            }

            return disabilities;
        }
    }
}
