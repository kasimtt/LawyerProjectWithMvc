using LawyerProject.Application.RequestParameters;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LawyerProject.Application.Features.Queries.Adverts.GetAllAdvert
{
    public class GetAllAdvertQueryRequest: IRequest<GetAllAdvertQueryResponse>
    {
        public Pagination Pagination { get; set; }
    }
}
