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
using PagedList;
using PagedList.Mvc;
using Microsoft.AspNet.Identity.EntityFramework;
using StudentPortal.Areas.Admin.ViewModels;
using StudentPortal.Domain.Roles;

namespace StudentPortal.Areas.Admin.Controllers
{
    public class UserManagementController : AdminController
    {
        private readonly IPortalRoleManager _portalRoleManager;

        public UserManagementController(IPortalRoleManager _portalRoleManager)
        {
            this._portalRoleManager = _portalRoleManager;
        }

        public ActionResult Default(string search, int? page)
        {
            var users = _ctx.Users
                .OrderBy(u => u.UserName)
                .Where(c => string.IsNullOrEmpty(search) || c.UserName.ToLower().Contains(search.ToLower()))
                .ToPagedList(page ?? 1, 25);

            return View(users);
        }

        [HttpGet]
        public async Task<ActionResult> Roles(string id)
        {
            ApplicationUser user = await _ctx.Users.SingleOrDefaultAsync(u => u.Id == id);
            if (user == null)
            {
                return RedirectToAction("Default");
            }

            List<IdentityRole> roles = await _ctx.Roles.ToListAsync();
            RoleViewModel model = new RoleViewModel(id, user.UserName);
            foreach (IdentityRole role in roles)
            {
                model.Roles.Add(role.Name);
                model.InRole.Add(_portalRoleManager.IsUserInRole(user, role.Name));
            }

            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> Roles(RoleViewModel model)
        {
            ApplicationUser user = await _ctx.Users.SingleOrDefaultAsync(u => u.Id == model.Id);
            if (user == null)
            {
                return RedirectToAction("Default");
            }

            List<IdentityRole> roles = await _ctx.Roles.ToListAsync();
            for (int roleIndex = 0; roleIndex < roles.Count; roleIndex++)
            {
                // Adding user to the role
                if (model.InRole[roleIndex])
                {
                    _portalRoleManager.AddUserToRole(user, roles[roleIndex].Name);
                }
                // Removing user from the role
                else
                {
                    _portalRoleManager.RemoveUserFromRole(user, roles[roleIndex].Name);
                }
            }

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
