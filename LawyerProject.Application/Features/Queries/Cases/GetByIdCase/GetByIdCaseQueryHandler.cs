using LawyerProject.Application.Repositories.CaseRepositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LawyerProject.Domain.Entities;
using LawyerProject.Application.DTOs.CasesDtos;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace LawyerProject.Application.Features.Queries.Cases.GetByIdCase
{
    public class GetByIdCaseQueryHandler : IRequestHandler<GetByIdCaseQueryRequest, GetByIdCaseQueryResponse>
    {
        private readonly ICaseReadRepository _caseReadRepository;
        private readonly IMapper _mapper;
        public GetByIdCaseQueryHandler(ICaseReadRepository caseReadRepository, IMapper mapper)
        {
            _caseReadRepository = caseReadRepository;
            _mapper = mapper;
        }
        public async Task<GetByIdCaseQueryResponse> Handle(GetByIdCaseQueryRequest request, CancellationToken cancellationToken)
        {
            Case? _case = await _caseReadRepository.Table.Include(c=>c.User).FirstOrDefaultAsync(c=>c.ObjectId == request.Id);

          // Case _case =  await _caseReadRepository.GetByIdAsync(request.Id);
           GetCaseDto getCaseDto = _mapper.Map<GetCaseDto>(_case);
            return new GetByIdCaseQueryResponse
            {
                GetCaseDtos = getCaseDto,
            };

        }
    }
}
