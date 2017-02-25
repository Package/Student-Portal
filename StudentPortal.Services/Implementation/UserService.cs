using StudentPortal.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;
using System.Threading.Tasks;
using StudentPortal.Domain.Context;
using StudentPortal.Domain.Models;

namespace StudentPortal.Services.Implementation
{
    public class UserService : IUserService
    {
        /// <summary>
        ///  Get the user details for the currently logged in user.
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="_ctx"></param>
        /// <returns></returns>
        public async Task<ApplicationUser> GetUser(string userName, StudentPortalContext _ctx)
        {
            return await _ctx.Users.SingleOrDefaultAsync(u => u.UserName == userName);
        }
    }
}
