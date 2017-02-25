using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentPortal.Domain.Models
{
    [Table("PersonalDetails")]
    public class PersonalDetails
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string Gender { get; set; }

        [Required]
        [StringLength(50)]
        public string Forename { get; set; }

        [Required]
        [StringLength(50)]
        public string Surname { get; set; }

        [Required]
        [EmailAddress]
        [StringLength(100)]
        [Display(Name = "Email address")]
        public string EmailAddress { get; set; }

        [Required]
        [Display(Name = "Date of birth (dd/mm/yyyy)")]
        [RegularExpression("^([0]?[0-9]|[12][0-9]|[3][01])[./-]([0]?[1-9]|[1][0-2])[./-]([0-9]{4}|[0-9]{2})$", ErrorMessage = "Please provide your date of birth in the following format: dd/mm/yyyy")]
        //[DataType(DataType.Date)]
        public string DateOfBirth { get; set; }

        [Display(Name = "Mobile number")]
        [RegularExpression("^[0-9 ]*$", ErrorMessage = "Your mobile number can only contain numbers")]
        [StringLength(20)]
        public string MobileNumber { get; set; }

        [Required]
        [Display(Name = "Telephone number")]
        [RegularExpression("^[0-9 ]*$", ErrorMessage = "Your telephone number can only contain numbers")]
        [StringLength(20)]
        public string TelephoneNumber { get; set; }

        [Required]
        [Display(Name = "Emergency contact name")]
        [StringLength(50)]
        public string NextOfKinName { get; set; }

        [Required]
        [Display(Name = "Emergency contact number")]
        [RegularExpression("^[0-9 ]*$", ErrorMessage = "Your emergency contact number can only contain numbers")]
        [StringLength(20)]
        public string NextOfKinContactNumber { get; set; }

        [Required]
        [Display(Name = "What is your nationality?")]
        public string Nationality { get; set; }

        [Required]
        [Display(Name = "What is your ethnicity?")]
        public string Ethnicity { get; set; }

        [Required]
        [Display(Name = "Which country do you live in?")]
        public string Country { get; set; }

        public virtual Application Application { get; set; }
    }
}
