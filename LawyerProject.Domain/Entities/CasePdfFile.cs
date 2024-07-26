using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LawyerProject.Domain.Entities
{
    public class CasePdfFile: File
    {
        public ICollection<Case>? Cases { get; set; }  
    }
}
