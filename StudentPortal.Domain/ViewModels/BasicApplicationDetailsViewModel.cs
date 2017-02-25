using StudentPortal.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentPortal.Domain.ViewModels
{
    public class BasicApplicationDetailsViewModel
    {
        public Application Application { get; set; }

        public PersonalDetails PersonalDetails { get; set; }

        public CourseSelection CourseSelection { get; set; }
    }
}
