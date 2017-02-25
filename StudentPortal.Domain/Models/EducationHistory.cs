using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentPortal.Domain.Models
{
    [Table("EducationHistory")]
    public class EducationHistory
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Display(Name = "Present or most recent School/College")]
        public string SchoolOrCollege { get; set; }

        [Display(Name = "What year were you last in education?")]
        public int? LastYear { get; set; }

        public virtual Application Application { get; set; }
    }
}
