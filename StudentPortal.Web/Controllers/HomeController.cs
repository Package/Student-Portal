using Postal;
using StudentPortal.Domain.Context;
using StudentPortal.Domain.Models;
using StudentPortal.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace StudentPortal.Controllers
{
    [Authorize]
    public class HomeController : BaseApplicationController
    {
        private readonly IMailService _mailService;

        public HomeController(IMailService _mailService)
        {
            this._mailService = _mailService;
        }

        public HomeController() : base()
        {
            // ...
        }

        [AllowAnonymous]
        public ActionResult Default()
        {
            // Logged in users should see the application dashboard as their home screen.
            if (Request.IsAuthenticated)
            {
                return RedirectToAction("Default", "Applications");
            }

            return View();
        }

        [AllowAnonymous]
        public async Task<string> Test()
        {
            await _mailService.SendEmailAsync(
                emailAddress: "ryan.dean1000@outlook.com", 
                subject: "This is a test from an async call", 
                message: null,
                emailTemplate: "Test", 
                parameters: null
           );
        
            return "Done";
        }

        public ActionResult Thanks()
        {
            return View();
        }
    }
}