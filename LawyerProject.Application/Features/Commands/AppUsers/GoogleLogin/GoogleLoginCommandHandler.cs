
using LawyerProject.Application.Abstractions.Services;
using LawyerProject.Application.DTOs.TokenDtos;
using MediatR;

namespace LawyerProject.Application.Features.Commands.AppUsers.GoogleLogin
{
    public class GoogleLoginCommandHandler : IRequestHandler<GoogleLoginCommandRequest, GoogleLoginCommandResponse>
    {
        private readonly IAuthService _authService;

        public GoogleLoginCommandHandler(IAuthService authService)
        {
            _authService = authService;
        }

        public async Task<GoogleLoginCommandResponse> Handle(GoogleLoginCommandRequest request, CancellationToken cancellationToken)
        {

            Token token = await _authService.GoogleLoginAsync(request.IdToken, request.Provider, 10);

            return new GoogleLoginCommandResponse
            {
                Token = token,
            };


        }
    }
}
