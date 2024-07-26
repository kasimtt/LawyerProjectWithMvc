using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LawyerProject.Application.Features.Queries.Cases.GetByIdCase
{
    public class GetByIdCaseQueryRequest: IRequest<GetByIdCaseQueryResponse>
    {
        public int Id { get; set; }
    }
}
