using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentPortal.Domain.Models.Reports
{
    public class ApplicantDetails
    {
        public int Id { get; set; }
        [Display(Name = "Application started")]
        public DateTime Started { get; set; }

        [Display(Name = "Application date")]
        public DateTime Finished { get; set; }
        public string Gender { get; set; }
        public string Forename { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        [Display(Name = "Date of birth")]
        public string DateOfBirth { get; set; }
        [Display(Name = "Contact number")]
        public string MobileNumber { get; set; }
        public string TelephoneNumber { get; set; }
    }
}
