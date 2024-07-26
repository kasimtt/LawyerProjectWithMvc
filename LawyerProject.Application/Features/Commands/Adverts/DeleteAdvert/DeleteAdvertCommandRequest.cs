using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LawyerProject.Application.Features.Commands.Adverts.DeleteAdvert
{
    public class DeleteAdvertCommandRequest: IRequest<DeleteAdvertCommandResponse>
    {
        public int Id { get; set; }
    }
}
