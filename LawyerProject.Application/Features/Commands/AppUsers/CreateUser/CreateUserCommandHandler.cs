using AutoMapper;
using LawyerProject.Application.Abstractions.Services;
using LawyerProject.Application.DTOs.UserDtos;
using LawyerProject.Application.Exceptions;
using LawyerProject.Domain.Entities.Identity;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LawyerProject.Application.Features.Commands.AppUsers.CreateUser
{
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommadRequest, CreateUserCommandResponse>
    {
        
        private readonly IUserService _userService;
        private readonly IMapper _mapper;
        public CreateUserCommandHandler(IUserService userService, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }
        public async Task<CreateUserCommandResponse> Handle(CreateUserCommadRequest request, CancellationToken cancellationToken)
        {
           
            CreateUserDto createUserDto = _mapper.Map<CreateUserDto>(request);

           CreateUserResponseDto createUserResponseDto =  await _userService.CreateAsync(createUserDto);

            return new CreateUserCommandResponse()
            {
                Success = createUserResponseDto.Success,
                Message = createUserResponseDto.Message
            };
              
        }

       
    }
}
