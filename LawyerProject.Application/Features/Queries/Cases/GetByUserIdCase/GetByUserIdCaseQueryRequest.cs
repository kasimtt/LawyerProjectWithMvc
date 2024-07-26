using LawyerProject.Application.Features.Queries.Cases.GetByUserIdCase;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LawyerProject.Application.Features.Queries.Cases.GetByUserIdCase
{
    public class GetByUserIdCaseQueryRequest: IRequest<GetByUserIdCaseQueryResponse>
    {
        public string UserNameOrEmail { get; set; }
    }
}
