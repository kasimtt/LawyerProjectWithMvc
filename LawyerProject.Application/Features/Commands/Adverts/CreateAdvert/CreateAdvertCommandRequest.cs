using LawyerProject.Domain.Enums;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LawyerProject.Application.Features.Commands.Adverts.CreateAdvert
{
    public class CreateAdvertCommandRequest : IRequest<CreateAdvertCommandResponse>
    {
        public string UserNameOrEmail { get; set; }
        public string CaseType { get; set; } = string.Empty;
        public DateTime CaseDate { get; set; }
        public decimal Price { get; set; }
        public string City { get; set; }
        public string Address { get; set; }
        public string District { get; set; }
        public string CasePlace { get; set; }
        public string Description { get; set; }
    }
}
