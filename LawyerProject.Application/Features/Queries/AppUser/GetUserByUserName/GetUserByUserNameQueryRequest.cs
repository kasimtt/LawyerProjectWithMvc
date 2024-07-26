using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LawyerProject.Application.Features.Queries.AppUser.GetUserByUserName
{
    public class GetUserByUserNameQueryRequest : IRequest<GetUserByUserNameQueryResponse>
    {
        public string UserNameOrEmail { get; set; } = string.Empty;
    }
}
