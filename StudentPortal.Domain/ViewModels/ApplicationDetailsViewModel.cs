using StudentPortal.Domain.Models;
using StudentPortal.Domain.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StudentPortal.ViewModels
{
    public class ApplicationDetailsViewModel
    {
        public Application Application { get; set; }
        public PersonalDetails PersonalDetails { get; set; }
        public Address Address { get; set; }
        public CourseSelection CourseSelection { get; set; }
        public List<Qualification> Qualifications { get; set; }
        public EducationHistory Education { get; set; }
        public LearningSupport LearningSupport { get; set; }
        public List<UserDisability> UserDisabilities { get; set; }
        public string DisabilityString { get; set; }


        public List<ApplicationProgress> Progress { get; set; }
        public List<Title> Titles { get; set; }
        public List<Gender> Genders { get; set; }
        public List<Country> Countries { get; set; }
        public List<Ethnicity> Ethnicities { get; set; }
        public List<Course> Courses { get; set; }
        public List<School> Schools { get; set; }
        public List<Disability> Disabilities { get; set; }

        public bool Complete {
            get {
                return Application != null && PersonalDetails != null &&
                       Address != null && CourseSelection != null &&
                       Qualifications != null && Qualifications.Any() &&
                       Education != null && LearningSupport != null;
            }
        }
    }
}