using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentPortal.Domain.Models
{
    [Table("CourseSelections")]
    public class CourseSelection
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Display(Name = "1st choice course")]
        public int FirstChoice { get; set; }

        [Display(Name = "2nd choice course")]
        public int? SecondChoice { get; set; }

        [Display(Name = "3rd choice course")]
        public int? ThirdChoice { get; set; }

        [Display(Name = "4th choice course")]
        public int? FourthChoice { get; set; }

        public string Notes { get; set; }

        public virtual Application Application { get; set; }
    }
}
