using AutoMapper;
using LawyerProject.Application.Repositories;
using LawyerProject.Application.Repositories.AdvertRepositories;
using LawyerProject.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LawyerProject.Application.Features.Commands.Adverts.UpdateAdvert
{
    public class UpdateAdvertCommandHandler : IRequestHandler<UpdateAdvertCommandRequest, UpdateAdvertCommandResponse>
    {
        private readonly IAdvertWriteRepository _advertWriteRepository;
        private readonly IAdvertReadRepository _advertReadRepository;
        private readonly IMapper _mapper;


        public UpdateAdvertCommandHandler(IAdvertWriteRepository advertWriteRepository, IAdvertReadRepository advertReadRepository, IMapper mapper)
        {
            _advertWriteRepository = advertWriteRepository;
            _advertReadRepository = advertReadRepository;
            _mapper = mapper;
        }

        public async Task<UpdateAdvertCommandResponse> Handle(UpdateAdvertCommandRequest request, CancellationToken cancellationToken)
        {

            Advert TempAdvert = await _advertReadRepository.GetByIdAsync(request.ObjectId,false);
            

            Advert advert = _mapper.Map<Advert>(request); // bi ara create'yi değişmeyen kodları yaz
            advert.CreatedDate = TempAdvert.CreatedDate;
            advert.IdUserFK = TempAdvert.IdUserFK;
            bool result = _advertWriteRepository.Update(advert);

            if (result)
            {
                await _advertWriteRepository.SaveAsync();
            }

            return new();
        }
    }
}
