using LawyerProject.Application.DTOs.CasesDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LawyerProject.Application.Features.Queries.Cases.GetAllCase
{
    public class GetAllCaseQueryResponse
    {
        public int TotalCount { get; set; }
        public IEnumerable<GetCaseDto>? GetCasesDto { get; set; }
    }
}
