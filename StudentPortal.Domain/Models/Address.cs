using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentPortal.Domain.Models
{
    [Table("Addresses")]
    public class Address
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "Address line 1")]
        public string Line1 { get; set; }

        [Display(Name = "Address line 2")]
        [StringLength(50)]
        public string Line2 { get; set; }

        [Required]
        [StringLength(50)]
        public string Town { get; set; }

        [Required]
        [Display(Name = "Post/Zip code")]
        [DataType(DataType.PostalCode)]
        [StringLength(8, MinimumLength = 6, ErrorMessage = "Post/Zip code must be between {2} and {1} characters long")]
        public string PostCode { get; set; }

        [Required]
        [Display(Name = "Home telephone number")]
        [StringLength(20)]
        [RegularExpression("^[0-9 ]*$", ErrorMessage = "Your telephone number can only contain numbers")]
        public string TelephoneNumber { get; set; }

        public virtual Application Application { get; set; }
    }
}
