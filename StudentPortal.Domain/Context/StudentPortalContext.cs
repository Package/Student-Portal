using Microsoft.AspNet.Identity.EntityFramework;
using StudentPortal.Domain.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentPortal.Domain.Context
{
    public class StudentPortalContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Application> Applications { get; set; }
        public DbSet<PersonalDetails> PersonalDetails { get; set; }
        public DbSet<Address> Addressses { get; set; }
        public DbSet<QualificationType> QualificationTypes { get; set; }
        public DbSet<Qualification> Qualifications { get; set; }
        public DbSet<CourseSelection> CourseSelections { get; set; }
        public DbSet<EducationHistory> Education { get; set; }
        public DbSet<AdditionalSupport> AdditionalSupport { get; set; }
        public DbSet<CourseGroup> CourseGroups { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Disability> Disabilities { get; set; }
        public DbSet<LearningSupport> LearningSupport { get; set; }
        public DbSet<UserDisability> UserDisabilities { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<Ethnicity> Ethnicity { get; set; }
        public DbSet<Gender> Genders { get; set; }
        public DbSet<School> Schools { get; set; }
        public DbSet<Title> Titles { get; set; }
        public DbSet<Institution> Institutions { get; set; }
        public DbSet<ApplicationProgress> ApplicationProgress { get; set; }

        public StudentPortalContext()
                : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static StudentPortalContext Create()
        {
            return new StudentPortalContext();
        }
    }
}
