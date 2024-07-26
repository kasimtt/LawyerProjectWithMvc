using LawyerProject.Domain.Enums;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LawyerProject.Application.Features.Commands.Case.UpdateCase
{
    public class UpdateCaseCommandRequest : IRequest<UpdateCaseCommandResponse>
    {
        public int ObjectId { get; set; }
        public int CaseNumber { get; set; }
        public string CaseNot { get; set; } = string.Empty;
        public string CaseDescription { get; set; } = string.Empty;
        public string CaseType { get; set; } = string.Empty;
        public DateTime? CaseDate { get; set; }
    }
}
