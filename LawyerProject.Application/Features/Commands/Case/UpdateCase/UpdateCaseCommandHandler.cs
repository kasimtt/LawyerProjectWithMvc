using C = LawyerProject.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LawyerProject.Application.Repositories;
using LawyerProject.Application.Repositories.CaseRepositories;
using AutoMapper;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace LawyerProject.Application.Features.Commands.Case.UpdateCase
{
    public class UpdateCaseCommandHandler : IRequestHandler<UpdateCaseCommandRequest, UpdateCaseCommandResponse>
    {
        private readonly ICaseWriteRepository _caseWriteRepository;
        private readonly ICaseReadRepository _caseReadRepository;
        private readonly IMapper _mapper;

        public UpdateCaseCommandHandler(ICaseWriteRepository caseWriteRepository, IMapper mapper, ICaseReadRepository caseReadRepository)
        {
            _caseWriteRepository = caseWriteRepository;
            _mapper = mapper;
            _caseReadRepository = caseReadRepository;
        }
        public async Task<UpdateCaseCommandResponse> Handle(UpdateCaseCommandRequest request, CancellationToken cancellationToken)
        {
            C.Case tempCase = await _caseReadRepository.GetByIdAsync(request.ObjectId,false);

            C.Case _case = _mapper.Map<C.Case>(request);
            _case.IdUserFK = tempCase.IdUserFK;
            _case.CreatedDate = tempCase.CreatedDate;

            _caseWriteRepository.Update(_case);

            await _caseWriteRepository.SaveAsync();


            return new();
        }
    }
}
