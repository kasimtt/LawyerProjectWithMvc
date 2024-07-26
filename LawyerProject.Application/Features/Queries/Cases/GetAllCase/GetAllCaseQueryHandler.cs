using AutoMapper;
using LawyerProject.Application.DTOs.CasesDtos;
using LawyerProject.Application.Repositories.AdvertRepositories;
using LawyerProject.Application.Repositories.CaseRepositories;
using LawyerProject.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LawyerProject.Application.Features.Queries.Cases.GetAllCase
{
    public class GetAllCaseQueryHandler : IRequestHandler<GetAllCaseQueryRequest, GetAllCaseQueryResponse>
    {
        private readonly ICaseReadRepository _caseReadRepository;
        private readonly IMapper _mapper;
        public GetAllCaseQueryHandler(ICaseReadRepository caseReadRepository, IMapper mapper)
        {
            _caseReadRepository = caseReadRepository;
            _mapper = mapper;
        }
        public async Task<GetAllCaseQueryResponse> Handle(GetAllCaseQueryRequest request, CancellationToken cancellationToken)
        {
            int totalCount = _caseReadRepository.GetAll(false).Count();
            var result = _caseReadRepository.GetAll(false).Skip(request.Pagination.Page * request.Pagination.Size)
                .Take(request.Pagination.Size).ToList();
            IEnumerable<GetCaseDto> entityDto = _mapper.Map<IEnumerable<Case>, IEnumerable<GetCaseDto>>(result).ToList();

            return new()
            {
                TotalCount = totalCount,
                GetCasesDto = entityDto
            };


        }
    }
}
