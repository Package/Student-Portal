using StudentPortal.Domain.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentPortal.Domain.Models
{
    [Table("Courses")]
    public class Course : ISelectable
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Code { get; set; }

        [Required]
        public string Title { get; set; }

        public int Type { get; set; }

        public string Information { get; set; }

        [Display(Name = "Start date")]
        [DataType(DataType.Date)]
        public DateTime? ActiveFrom { get; set; }

        [Display(Name = "End date")]
        [DataType(DataType.Date)]
        public DateTime? ActiveTo { get; set; }

        public bool Active { get; set; }

        [NotMapped]
        public string Name { get { return Title; } set { Title = value; } }
    }
}
