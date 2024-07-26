using LawyerProject.Application.DTOs.CasesDtos;
using LawyerProject.Domain.Enums;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LawyerProject.Application.Features.Commands.CreateCase
{
    public class CreateCaseCommandRequest: IRequest<CreateCaseCommandResponse>
    {
        public string UserNameOrEmail { get; set; }
        public int CaseNumber { get; set; }
        public string? CaseNot { get; set; } 
        public string? CaseDescription { get; set; } 
        public string CaseType { get; set; } = string.Empty;
        public DateTime? CaseDate { get; set; }
    }
}
