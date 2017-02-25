using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentPortal.Domain.Models
{
    [Table("UserDisabilities")]
    public class UserDisability
    {
        [Key]
        public int Id { get; set; }

        public int Disability { get; set; }

        public virtual Application Application { get; set; }
    }
}
