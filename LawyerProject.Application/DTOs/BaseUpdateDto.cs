using LawyerProject.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LawyerProject.Application.DTOs
{
    public class BaseUpdateDto
    {
        public int ObjectId { get; set; }
        public DateTime? UpdatedDate { get; set; }
        
    }
}
