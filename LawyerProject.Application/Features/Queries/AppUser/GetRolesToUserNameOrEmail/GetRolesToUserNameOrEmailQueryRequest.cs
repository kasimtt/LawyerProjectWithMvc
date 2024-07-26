using MediatR;

namespace LawyerProject.Application.Features.Queries.AppUser.GetRolesToUserNameOrEmail
{
    public class GetRolesToUserNameOrEmailQueryRequest : IRequest<GetRolesToUserNameOrEmailQueryResponse>
    {
        public string UserNameOrEmail { get; set; }
    }
}