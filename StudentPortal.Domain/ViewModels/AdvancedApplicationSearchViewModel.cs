using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentPortal.Domain.ViewModels
{
    public class AdvancedApplicationSearchViewModel
    {
        [Display(Name = "Search query")]
        public string Search { get; set; }

        [Display(Name = "Application status")]
        public int? ProgressId { get; set; }

        [Display(Name = "Applied between (dd/mm/yyyy)")]
        [RegularExpression("^([0]?[0-9]|[12][0-9]|[3][01])[./-]([0]?[1-9]|[1][0-2])[./-]([0-9]{4}|[0-9]{2})$", ErrorMessage = "The date must be in the following format: dd/mm/yyyy")]
        public string StartDate { get; set; }

        [Display(Name = "And (dd/mm/yyyy)")]
        [RegularExpression("^([0]?[0-9]|[12][0-9]|[3][01])[./-]([0]?[1-9]|[1][0-2])[./-]([0-9]{4}|[0-9]{2})$", ErrorMessage = "The date must be in the following format: dd/mm/yyyy")]
        public string EndDate { get; set; }

        [Display(Name = "Show applications for this course")]
        public int? CourseId { get; set; }

        [Display(Name = "Show applications for this course group")]
        public int? CourseGroupId { get; set; }

        public int? Page { get; set; }
    }
}
