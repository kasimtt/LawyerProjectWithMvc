using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LawyerProject.Application.Features.Queries.CasePdfFiles.GetCasePdfFile
{
    public class GetCasePdfFileQueryRequest : IRequest<List<GetCasePdfFileQueryResponse>>
    {
        public int Id { get; set; }
    }
}
