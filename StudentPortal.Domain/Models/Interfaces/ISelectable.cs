using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentPortal.Domain.Models.Interfaces
{
    public interface ISelectable
    {
        int Id { get; set; }

        string Name { get; set; }
    }
}
