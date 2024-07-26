using LawyerProject.Application.Abstractions.Services;
using LawyerProject.Application.Exceptions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LawyerProject.Application.Features.Commands.AppUsers.PasswordUpdate
{
    public class PasswordUpdateCommandHandler : IRequestHandler<PasswordUpdateCommandRequest, PasswordUpdateCommandResponse>
    {
        readonly IUserService _userService;

        public PasswordUpdateCommandHandler(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<PasswordUpdateCommandResponse> Handle(PasswordUpdateCommandRequest request, CancellationToken cancellationToken)
        {
            if (!request.Password.Equals(request.PasswordConfirm))
            {
                throw new PasswordChangeFailedException("Lütfen şifreyi doğrulayınız.");
            }

            await _userService.UpdatePasswordAsync(request.UserId, request.ResetToken, request.Password);

            return new();
        }
    }
}
