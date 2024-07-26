using LawyerProject.Application.Features.Commands.CreateRole;
using MediatR;

namespace LawyerProject.Application.Features.Commands.Role.CreateRole
{
    public class CreateRoleCommandRequest : IRequest<CreateRoleCommandResponse>
    {
        public string Name { get; set; }
    }
}