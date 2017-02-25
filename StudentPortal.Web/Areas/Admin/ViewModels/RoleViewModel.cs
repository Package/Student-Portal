using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StudentPortal.Areas.Admin.ViewModels
{
    public class RoleViewModel
    {
        public string Id { get; set; }

        public string UserName { get; set; }

        public List<string> Roles { get; set; }

        public List<bool> InRole { get; set; }

        public RoleViewModel() 
        {
        }

        public RoleViewModel(string id, string userName)
        {
            this.Id = id;
            this.UserName = userName;
            this.Roles = new List<string>();
            this.InRole = new List<bool>();
        }
    }
}