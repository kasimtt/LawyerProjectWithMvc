using LawyerProject.Application.Exceptions;
using LawyerProject.Application.Repositories.CaseRepositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using  C = LawyerProject.Domain.Entities;
namespace LawyerProject.Application.Features.Commands.Case.DeleteCase
{
    public class DeleteCaseCommandHandler : IRequestHandler<DeleteCaseCommandRequest, DeleteCaseCommandResponse>
    {
        private readonly ICaseReadRepository _caseReadRepository;
        private readonly ICaseWriteRepository _caseWriteRepository;
        public DeleteCaseCommandHandler(ICaseReadRepository caseReadRepository, ICaseWriteRepository caseWriteRepository)
        {
            _caseReadRepository = caseReadRepository;
            _caseWriteRepository = caseWriteRepository;
        }

        public async Task<DeleteCaseCommandResponse> Handle(DeleteCaseCommandRequest request, CancellationToken cancellationToken)
        {
           C.Case _case = await _caseReadRepository.GetByIdAsync(request.Id);
            if(_case == null)
            {
                throw new NotFoundCaseException();
            }

            _case.DataState = Domain.Enums.DataState.Deleted;
            await _caseWriteRepository.SaveAsync();
            return new DeleteCaseCommandResponse { Success = true };
            

        }
    }
}
