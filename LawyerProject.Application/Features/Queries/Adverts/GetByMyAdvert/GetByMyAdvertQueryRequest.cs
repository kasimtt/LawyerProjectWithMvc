using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LawyerProject.Application.Features.Queries.Adverts.GetByIdAdvert
{
    public class GetByMyAdvertQueryRequest : IRequest<GetByMyAdvertQueryResponse>
    {
        public string UserNameOrEmail { get; set; }
    }
}
