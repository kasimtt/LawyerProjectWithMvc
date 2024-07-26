using LawyerProject.Application.Abstractions.Services;
using LawyerProject.Application.Abstractions.Token;
using LawyerProject.Application.DTOs.TokenDtos;
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

namespace LawyerProject.Application.Features.Commands.AppUsers.LoginUser
{
    public class LoginUserCommandHandler : IRequestHandler<LoginUserCommandRequest, LoginUserCommandResponse>
    {
        private IAuthService _authService;
        private readonly UserManager<AppUser> _userManager;
        public LoginUserCommandHandler(IAuthService authService, UserManager<AppUser> userManager)
        {
            _authService = authService;
            _userManager = userManager;
        }

        public async Task<LoginUserCommandResponse> Handle(LoginUserCommandRequest request, CancellationToken cancellationToken)
        {

           Token token = await _authService.LoginAsync(request.UserNameOrEmail, request.Password, 10);

            if (token != null)
            {
                AppUser user = await _userManager.FindByEmailAsync(request.UserNameOrEmail);
                GetUserDto getUserDto = new GetUserDto
                {
                    PhoneNumber = user.PhoneNumber,
                    Email = user.Email,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Id = user.Id
                };
                return new LoginUserCommandResponse { Token = token , User=getUserDto };
            }

           

            return new LoginUserCommandResponse { Token  = token };
            

        }
    }
}
