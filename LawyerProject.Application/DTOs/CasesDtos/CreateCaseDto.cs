using LawyerProject.Domain.Entities;
using LawyerProject.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LawyerProject.Application.DTOs.CasesDtos
{
    public class CreateCaseDto : BaseCreateDto
    {
        public int IdUserFK { get; set; }
        public int CaseNumber { get; set; }
        public string CaseNot { get; set; } = string.Empty;
        public string CaseDescription { get; set; } = string.Empty;
        public string CaseType { get; set; } = string.Empty;
        public DateTime? CaseDate { get; set; }


    }
}
