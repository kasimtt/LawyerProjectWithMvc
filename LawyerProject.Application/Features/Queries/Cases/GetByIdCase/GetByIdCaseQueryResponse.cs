using LawyerProject.Application.DTOs.CasesDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LawyerProject.Application.Features.Queries.Cases.GetByIdCase
{
    public class GetByIdCaseQueryResponse
    {
        public GetCaseDto? GetCaseDtos { get; set; }
    }
}
