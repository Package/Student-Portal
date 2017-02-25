namespace StudentPortal.Domain.Migrations
{
    using Context;
    using Roles;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<StudentPortalContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
            ContextKey = "StudentPortal.Domain.Context.StudentPortalContext";
        }

        protected override void Seed(StudentPortalContext context)
        {
            IPortalRoleManager roleManager = new PortalRoleManager(context);
            roleManager.SetupPortalRoles();
            roleManager.SetupAdministratorAccount();
        }
    }
}
