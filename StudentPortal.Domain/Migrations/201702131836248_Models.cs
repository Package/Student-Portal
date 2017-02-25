namespace StudentPortal.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Models : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AdditionalSupport",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Disability = c.String(),
                        Identified = c.Boolean(nullable: false),
                        Application_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Applications", t => t.Application_Id)
                .Index(t => t.Application_Id);
            
            CreateTable(
                "dbo.Applications",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Complete = c.Boolean(nullable: false),
                        Submitted = c.Boolean(nullable: false),
                        State = c.String(),
                        Started = c.DateTime(nullable: false),
                        Finished = c.DateTime(),
                        User_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.User_Id)
                .Index(t => t.User_Id);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.Addresses",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Line1 = c.String(nullable: false, maxLength: 50),
                        Line2 = c.String(maxLength: 50),
                        Town = c.String(nullable: false, maxLength: 50),
                        PostCode = c.String(nullable: false, maxLength: 8),
                        TelephoneNumber = c.String(nullable: false, maxLength: 20),
                        Application_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Applications", t => t.Application_Id)
                .Index(t => t.Application_Id);
            
            //CreateTable(
            //    "dbo.Countries",
            //    c => new
            //        {
            //            Id = c.Int(nullable: false, identity: true),
            //            Name = c.String(nullable: false),
            //            MISCode = c.String(),
            //        })
            //    .PrimaryKey(t => t.Id);
            
            //CreateTable(
            //    "dbo.CourseGroups",
            //    c => new
            //        {
            //            Id = c.Int(nullable: false, identity: true),
            //            Name = c.String(nullable: false),
            //        })
            //    .PrimaryKey(t => t.Id);
            
            //CreateTable(
            //    "dbo.Courses",
            //    c => new
            //        {
            //            Id = c.Int(nullable: false, identity: true),
            //            Code = c.String(nullable: false),
            //            Title = c.String(nullable: false),
            //            Type = c.Int(nullable: false),
            //            Information = c.String(),
            //            ActiveFrom = c.DateTime(),
            //            ActiveTo = c.DateTime(),
            //            Active = c.Boolean(nullable: false),
            //        })
            //    .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.CourseSelections",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FirstChoice = c.Int(nullable: false),
                        SecondChoice = c.Int(),
                        ThirdChoice = c.Int(),
                        FourthChoice = c.Int(),
                        Notes = c.String(),
                        Application_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Applications", t => t.Application_Id)
                .Index(t => t.Application_Id);
            
            //CreateTable(
            //    "dbo.Disabilities",
            //    c => new
            //        {
            //            Id = c.Int(nullable: false, identity: true),
            //            Name = c.String(nullable: false),
            //        })
            //    .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.EducationHistory",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        SchoolOrCollege = c.String(nullable: false),
                        LastYear = c.Int(),
                        Application_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Applications", t => t.Application_Id)
                .Index(t => t.Application_Id);
            
            //CreateTable(
            //    "dbo.Ethnicities",
            //    c => new
            //        {
            //            Id = c.Int(nullable: false, identity: true),
            //            Name = c.String(nullable: false),
            //            MISCode = c.String(),
            //        })
            //    .PrimaryKey(t => t.Id);
            
            //CreateTable(
            //    "dbo.Gender",
            //    c => new
            //        {
            //            Id = c.Int(nullable: false, identity: true),
            //            Name = c.String(nullable: false),
            //            MISCode = c.String(),
            //        })
            //    .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.LearningSupport",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CriminalConviction = c.Boolean(nullable: false),
                        HasDisability = c.Boolean(nullable: false),
                        ReceivedLearningSupport = c.Boolean(nullable: false),
                        SENStatement = c.Boolean(nullable: false),
                        ExamAccessArrangements = c.Boolean(nullable: false),
                        Notes = c.String(),
                        Application_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Applications", t => t.Application_Id)
                .Index(t => t.Application_Id);
            
            CreateTable(
                "dbo.PersonalDetails",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false),
                        Gender = c.String(nullable: false),
                        Forename = c.String(nullable: false, maxLength: 50),
                        Surname = c.String(nullable: false, maxLength: 50),
                        EmailAddress = c.String(nullable: false, maxLength: 100),
                        DateOfBirth = c.String(nullable: false),
                        MobileNumber = c.String(maxLength: 20),
                        TelephoneNumber = c.String(nullable: false, maxLength: 20),
                        NextOfKinName = c.String(nullable: false, maxLength: 50),
                        NextOfKinContactNumber = c.String(nullable: false, maxLength: 20),
                        Nationality = c.String(nullable: false),
                        Ethnicity = c.String(nullable: false),
                        Country = c.String(nullable: false),
                        Application_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Applications", t => t.Application_Id)
                .Index(t => t.Application_Id);
            
            CreateTable(
                "dbo.Qualifications",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Type = c.String(nullable: false),
                        PredictedGrade = c.String(),
                        ActualGrade = c.String(nullable: false),
                        Application_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Applications", t => t.Application_Id)
                .Index(t => t.Application_Id);
            
            //CreateTable(
            //    "dbo.QualificationType",
            //    c => new
            //        {
            //            Id = c.Int(nullable: false, identity: true),
            //            Name = c.String(nullable: false),
            //        })
            //    .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            //CreateTable(
            //    "dbo.Schools",
            //    c => new
            //        {
            //            Id = c.Int(nullable: false, identity: true),
            //            Name = c.String(nullable: false),
            //            MISCode = c.String(),
            //        })
            //    .PrimaryKey(t => t.Id);
            
            //CreateTable(
            //    "dbo.Titles",
            //    c => new
            //        {
            //            Id = c.Int(nullable: false, identity: true),
            //            Name = c.String(nullable: false),
            //            MISCode = c.String(),
            //        })
            //    .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.UserDisabilities",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Disability = c.Int(nullable: false),
                        Application_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Applications", t => t.Application_Id)
                .Index(t => t.Application_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserDisabilities", "Application_Id", "dbo.Applications");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.Qualifications", "Application_Id", "dbo.Applications");
            DropForeignKey("dbo.PersonalDetails", "Application_Id", "dbo.Applications");
            DropForeignKey("dbo.LearningSupport", "Application_Id", "dbo.Applications");
            DropForeignKey("dbo.EducationHistory", "Application_Id", "dbo.Applications");
            DropForeignKey("dbo.CourseSelections", "Application_Id", "dbo.Applications");
            DropForeignKey("dbo.Addresses", "Application_Id", "dbo.Applications");
            DropForeignKey("dbo.AdditionalSupport", "Application_Id", "dbo.Applications");
            DropForeignKey("dbo.Applications", "User_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropIndex("dbo.UserDisabilities", new[] { "Application_Id" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.Qualifications", new[] { "Application_Id" });
            DropIndex("dbo.PersonalDetails", new[] { "Application_Id" });
            DropIndex("dbo.LearningSupport", new[] { "Application_Id" });
            DropIndex("dbo.EducationHistory", new[] { "Application_Id" });
            DropIndex("dbo.CourseSelections", new[] { "Application_Id" });
            DropIndex("dbo.Addresses", new[] { "Application_Id" });
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.Applications", new[] { "User_Id" });
            DropIndex("dbo.AdditionalSupport", new[] { "Application_Id" });
            DropTable("dbo.UserDisabilities");
            DropTable("dbo.Titles");
            DropTable("dbo.Schools");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.QualificationType");
            DropTable("dbo.Qualifications");
            DropTable("dbo.PersonalDetails");
            DropTable("dbo.LearningSupport");
            DropTable("dbo.Gender");
            DropTable("dbo.Ethnicities");
            DropTable("dbo.EducationHistory");
            DropTable("dbo.Disabilities");
            DropTable("dbo.CourseSelections");
            DropTable("dbo.Courses");
            DropTable("dbo.CourseGroups");
            DropTable("dbo.Countries");
            DropTable("dbo.Addresses");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.Applications");
            DropTable("dbo.AdditionalSupport");
        }
    }
}
