using System.Web.Mvc;
using Microsoft.Practices.Unity;
using Unity.Mvc3;
using StudentPortal.Services.Interfaces;
using StudentPortal.Services.Implementation;
using StudentPortal.Controllers;
using StudentPortal.Domain.Roles;

namespace StudentPortal
{
    public static class Bootstrapper
    {
        public static void Initialise()
        {
            var container = BuildUnityContainer();

            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }

        private static IUnityContainer BuildUnityContainer()
        {
            var container = new UnityContainer();
            container.RegisterType<AccountController>(new InjectionConstructor());
            container.RegisterType<ManageController>(new InjectionConstructor());

            container.RegisterType<IApplicationService, ApplicationService>();
            container.RegisterType<ILearningSupportService, LearningSupportService>();
            container.RegisterType<IDisabilityService, DisabilityService>();
            container.RegisterType<IUserService, UserService>();
            container.RegisterType<IReportService, ReportService>();
            container.RegisterType<IMailService, MailService>();

            container.RegisterType<IPortalRoleManager, PortalRoleManager>();

            return container;
        }
    }
}