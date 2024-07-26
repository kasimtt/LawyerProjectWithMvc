using LawyerProject.Application.Repositories.AdvertRepositories;
using LawyerProject.Domain.Entities;
using MediatR;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LawyerProject.Application.Features.Commands.Adverts.DeleteAdvert
{
    public class DeleteAdvertCommandHandler : IRequestHandler<DeleteAdvertCommandRequest, DeleteAdvertCommandResponse>
    {
        private readonly IAdvertReadRepository _advertReadRepository;
        private readonly IAdvertWriteRepository _advertWriteRepository;

        public DeleteAdvertCommandHandler(IAdvertReadRepository advertReadRepository, IAdvertWriteRepository advertWriteRepository)
        {
            _advertReadRepository = advertReadRepository;
            _advertWriteRepository = advertWriteRepository;
        }

        public async Task<DeleteAdvertCommandResponse> Handle(DeleteAdvertCommandRequest request, CancellationToken cancellationToken)
        {
           Advert advert =  await _advertReadRepository.GetByIdAsync(request.Id);
            advert.DataState = Domain.Enums.DataState.Deleted;
            await _advertWriteRepository.SaveAsync();
            return new DeleteAdvertCommandResponse
            {
                Success = true,
            };


            /* C.Case _case = await _caseReadRepository.GetByIdAsync(request.Id);
            _case.DataState = Domain.Enums.DataState.Deleted;
            await _caseWriteRepository.SaveAsync();
            return new DeleteCaseCommandResponse { Success = true };*/
        }
    }
}
