using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LawyerProject.Application.Features.Commands.Case.DeleteCase
{
    public class DeleteCaseCommandRequest : IRequest<DeleteCaseCommandResponse>
    {
        public int Id { get; set; }
    }
}
