using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LawyerProject.Application.Features.Queries.AppUser.GetUserForProfile
{
    public class GetUserDetailsByUserEmailQueryRequest: IRequest<GetUserDetailsByUserEmailQueryResponse>
    {
        public string UserNameOrEmail { get; set; } = string.Empty;
    }
}
