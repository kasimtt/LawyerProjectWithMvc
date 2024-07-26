using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LawyerProject.Application.Features.Commands.CasePdfFiles.RemoveCasePdfFile
{
    public class RemoveCasePdfFileCommandRequest : IRequest<RemoveCasePdfFileCommandResponse>
    {
        public int Id { get; set; } 
        public int ImageId { get; set; }
    }
}
