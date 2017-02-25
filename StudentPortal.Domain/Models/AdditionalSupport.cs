using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentPortal.Domain.Models
{
    [Table("AdditionalSupport")]
    public class AdditionalSupport
    {
        [Key]
        public int Id { get; set; }

        public string Disability { get; set; }

        public bool Identified { get; set; }

        public virtual Application Application { get; set; }
    }
}
