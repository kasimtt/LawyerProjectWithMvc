using MediatR;

namespace LawyerProject.Application.Features.Commands.AppUsers.AssignRoleToUser
{
    public class AssignRoleToUserCommandRequest :IRequest<AssignRoleToUserCommandResponse>
    {
        public string UserId { get; set; }
        public string[] Roles { get; set; }
    }
}