using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentPortal.Domain.Models
{
    [Table("LearningSupport")]
    public class LearningSupport
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "I have previously been convicted of a criminal offence")]
        public bool CriminalConviction { get; set; }

        [Display(Name = "I consider myself to have a learning difficulty or disability")]
        public bool HasDisability { get; set; }

        [Display(Name = "I have received learning support in the past")]
        public bool ReceivedLearningSupport { get; set; }

        [Display(Name = "I have a statement for Special Educational Needs (SEN) / Educational health care plan")]
        public bool SENStatement { get; set; }

        [Display(Name = "I have previously received Access Arragements for Examinations")]
        public bool ExamAccessArrangements { get; set; }

        [NotMapped]
        public List<Disability> Disability { get; set; }

        public string Notes { get; set; }

        public virtual Application Application { get; set; }
    }
}
