using AutoMapper;
using LawyerProject.Application.Abstractions.Hubs;
using LawyerProject.Application.Exceptions;
using LawyerProject.Application.Repositories.AdvertRepositories;
using LawyerProject.Domain.Entities;
using LawyerProject.Domain.Entities.Identity;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LawyerProject.Application.Features.Commands.Adverts.CreateAdvert
{
    public class CreateAdvertCommandHandler : IRequestHandler<CreateAdvertCommandRequest, CreateAdvertCommandResponse>
    {
        private readonly IAdvertWriteRepository _advertWriteRepository;
        private readonly IMapper _mapper;
        private readonly UserManager<AppUser> _userManager;
        private readonly IAdvertHubService _advertHubService;

        public CreateAdvertCommandHandler(IAdvertWriteRepository advertWriteRepository, IMapper mapper, UserManager<AppUser> userManager, IAdvertHubService advertHubService)
        {
            _advertWriteRepository = advertWriteRepository;
            _mapper = mapper;
            _userManager = userManager;
            _advertHubService = advertHubService;
        }

        public async Task<CreateAdvertCommandResponse> Handle(CreateAdvertCommandRequest request, CancellationToken cancellationToken)
        {
            AppUser user = await _userManager.FindByEmailAsync(request.UserNameOrEmail);
            if (user == null)
            {
                user = await _userManager.FindByNameAsync(request.UserNameOrEmail);
                if (user == null)
                {
                    throw new NotFoundUserException();
                }
            }

            Advert advert = _mapper.Map<Advert>(request);

            advert.IdUserFK = user.Id;

            bool result = await _advertWriteRepository.AddAsync(advert);
            if (result)
            {
                await _advertWriteRepository.SaveAsync();
            }
            await _advertHubService.AdvertAddedMessageAsync("Yeni eklenen ilanlar var!");
            return new CreateAdvertCommandResponse
            {
                Success = result,
            };




        }
    }
}
