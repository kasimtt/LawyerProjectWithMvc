using AutoMapper;
using LawyerProject.Application.DTOs.CasesDtos;
using LawyerProject.Application.Features.Queries.Cases.GetByUserIdCase;
using LawyerProject.Application.Repositories.CaseRepositories;
using LawyerProject.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using A = LawyerProject.Domain.Entities.Identity;

namespace LawyerProject.Application.Features.Queries.Cases.GetByUserIdCase
{
    public class GetByUserIdCaseQueryHandler : IRequestHandler<GetByUserIdCaseQueryRequest, GetByUserIdCaseQueryResponse>
    {
        private readonly ICaseReadRepository _caseReadRepository;
        private readonly IMapper _mapper;
        private readonly UserManager<A.AppUser> _userManager;

        public GetByUserIdCaseQueryHandler(ICaseReadRepository caseReadRepository, IMapper mapper, UserManager<A.AppUser> userManager)
        {
            _caseReadRepository = caseReadRepository;
            _mapper = mapper;
            _userManager = userManager;
        }

        public async Task<GetByUserIdCaseQueryResponse> Handle(GetByUserIdCaseQueryRequest request, CancellationToken cancellationToken)
        {
            A.AppUser user = await _userManager.FindByEmailAsync(request.UserNameOrEmail);
            if (user == null)
            {
                user = await _userManager.FindByNameAsync(request.UserNameOrEmail);
            }

            IEnumerable<Case> cases = _caseReadRepository.Table.Include(c => c.User).
                Where(c => c.IdUserFK == user.Id && c.DataState == Domain.Enums.DataState.Active).ToList();
            IEnumerable<GetCaseDto> getCaseDtos = _mapper.Map<IEnumerable<Case>, IEnumerable<GetCaseDto>>(cases).ToList();

            return new GetByUserIdCaseQueryResponse
            {
                GetCasesDto = getCaseDtos,
                TotalCount = getCaseDtos.Count()
            };
        }
    }
}
