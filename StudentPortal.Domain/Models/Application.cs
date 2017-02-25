using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentPortal.Domain.Models
{
    [Table("Applications")]
    public class Application
    {
        public int Id { get; set; }

        public virtual ApplicationUser User { get; set; }

        public bool Complete { get; set; }

        public bool Submitted { get; set; }

        public string State { get; set; }

        public int Progress { get; set; }

        public DateTime Started { get; set; }

        public DateTime? Finished { get; set; }
    }
}
