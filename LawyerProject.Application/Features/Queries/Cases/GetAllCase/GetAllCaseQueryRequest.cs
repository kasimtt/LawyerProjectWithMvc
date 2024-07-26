using LawyerProject.Application.RequestParameters;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LawyerProject.Application.Features.Queries.Cases.GetAllCase
{
    public class GetAllCaseQueryRequest : IRequest<GetAllCaseQueryResponse>
    {
        public Pagination Pagination { get; set; }
    }
}
