using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentPortal.Domain.Models
{
    [Table("InstitutionDetails")]
    public class Institution
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Display(Name= "Institution name")]
        public string Name { get; set; }

        [Display(Name= "Institution logo")]
        public string Logo { get; set; }

        [Required]
        [Display(Name = "General institution contact number")]
        public string ContactNumber { get; set; }

        [Required]
        [Display(Name = "General institution contact email")]
        public string ContactEmail { get; set; }
    }
}
