using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentPortal.Domain.Models
{
    [Table("Qualifications")]
    public class Qualification
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Display(Name = "Name/Description")]
        public string Name { get; set; }

        [Required]
        public string Type { get; set; }

        [Display(Name = "Predicted grade (if applicable)")]
        public string PredictedGrade { get; set; }

        [Required(ErrorMessage = "Please enter the grade you achieved or 'N/A' if you are currently awaiting your results.")]
        [Display(Name = "Grade achieved")]
        public string ActualGrade { get; set; }

        public virtual Application Application { get; set; }
    }
}
