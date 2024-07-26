using MediatR;

namespace LawyerProject.Application.Features.Queries.AppUser.GetAllUsers
{
    public class GetAllUsersQueryReqeuest :IRequest<GetAllUsersQueryResponse>
    {
        public int Page { get; set; }
        public int Size { get; set; }
    }
}