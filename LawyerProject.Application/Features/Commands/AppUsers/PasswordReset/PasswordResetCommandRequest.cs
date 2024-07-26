using MediatR;

namespace LawyerProject.Application.Features.Commands.AppUsers.PasswordReset
{
    public class PasswordResetCommandRequest : IRequest<PasswordResetCommandResponse>
    {
        public string Email { get; set; }
    }
}