using AutoMapper;
using LawyerProject.Application.Exceptions;
using LawyerProject.Application.Repositories.CaseRepositories;
using LawyerProject.Domain.Entities;
using LawyerProject.Domain.Entities.Identity;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using C = LawyerProject.Domain.Entities;
namespace LawyerProject.Application.Features.Commands.CreateCase
{
    public class CreateCaseCommandHandler : IRequestHandler<CreateCaseCommandRequest, CreateCaseCommandResponse>
    {
        private readonly ICaseWriteRepository _caseWriteRepository;
        private readonly IMapper _mapper;
        private readonly UserManager<AppUser> _userManager;
        public CreateCaseCommandHandler(ICaseWriteRepository caseWriteRepository, IMapper mapper, UserManager<AppUser> userManager)
        {
            _caseWriteRepository = caseWriteRepository;
            _mapper = mapper;
            _userManager = userManager;
        }
        public async Task<CreateCaseCommandResponse> Handle(CreateCaseCommandRequest request, CancellationToken cancellationToken)
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

            C.Case _case = _mapper.Map<C.Case>(request);
            _case.IdUserFK = user.Id;

            await _caseWriteRepository.AddAsync(_case);
            await _caseWriteRepository.SaveAsync();

            return new CreateCaseCommandResponse
            {
                success = true,
            };
        }
    }
}
