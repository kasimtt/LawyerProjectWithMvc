using MediatR;

namespace LawyerProject.Application.Features.Commands.AppUsers.PasswordUpdate
{
    public class PasswordUpdateCommandRequest : IRequest<PasswordUpdateCommandResponse>
    {
        public string UserId { get; set; }
        public string ResetToken { get; set; }
        public string Password { get; set; }
        public string PasswordConfirm { get; set; }
    }
}