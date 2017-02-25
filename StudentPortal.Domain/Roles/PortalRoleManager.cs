using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using StudentPortal.Domain.Context;
using StudentPortal.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentPortal.Domain.Roles
{
    public interface IPortalRoleManager
    {
        bool IsUserInRole(ApplicationUser user, string role);

        void AddUserToRole(ApplicationUser user, string role);

        void RemoveUserFromRole(ApplicationUser user, string role);

        void SetupPortalRoles();

        void SetupAdministratorAccount();
    }

    public class PortalRoleManager : IPortalRoleManager
    {
        private readonly string[] PortalRoles = {
            "Admin", // Portal administrator
            "SMT", // Senior management team
            "Data", // Data manager
            "Admissions", // Admissions officer
            "Staff" // Regular institution staff    `
        };

        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<ApplicationUser> _userManager;

        public PortalRoleManager(StudentPortalContext _ctx)
        {
            this._roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(_ctx));
            this._userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(_ctx));
        }

        /// <summary>
        /// Create the roles used within the system if they do not exist.
        /// </summary>
        /// <returns></returns>
        public void SetupPortalRoles()
        {
            foreach (string role in PortalRoles)
            {
                if (!_roleManager.RoleExists(role))
                {
                    _roleManager.Create(new IdentityRole { Name = role });
                }
            }
        }

        /// <summary>
        /// Setup the inital administrator account and add it to the 'Admin' role
        /// </summary>
        /// <returns></returns>
        public void SetupAdministratorAccount()
        {
            ApplicationUser administrator = _userManager.FindByName("administrator@studentportal.com");
            if (administrator == null)
            {
                // No administrator account exists, set one up.
                administrator = new ApplicationUser { UserName = "administrator@studentportal.com", Email = "administrator@studentportal.com" };
                _userManager.Create(administrator, password: "studentP0rtal");

                // Add them into the admin role
                _userManager.AddToRole(administrator.Id, "Admin");
            }
        }

        /// <summary>
        /// Check if a provided user is in the provided role.
        /// </summary>
        /// <param name="user"></param>
        /// <param name="role"></param>
        /// <returns></returns>
        public bool IsUserInRole(ApplicationUser user, string role)
        {
            return _userManager.IsInRole(user.Id, role);
        }

        /// <summary>
        /// Add the provided user to the provided role if they aren't already
        /// in the role.
        /// </summary>
        /// <param name="user"></param>
        /// <param name="role"></param>
        public void AddUserToRole(ApplicationUser user, string role)
        {
            if (!IsUserInRole(user, role))
            {
                _userManager.AddToRole(user.Id, role);
            }
        }

        /// <summary>
        /// Remove the provided user from the provided role if they are in the role.
        /// </summary>
        /// <param name="user"></param>
        /// <param name="role"></param>
        public void RemoveUserFromRole(ApplicationUser user, string role)
        {
            if (IsUserInRole(user, role))
            {
                _userManager.RemoveFromRole(user.Id, role);
            }
        }
    }
}
