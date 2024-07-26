using LawyerProject.Application.Abstractions.Services;
using LawyerProject.Application.DTOs.UserDtos;
using LawyerProject.Application.Features.Queries.AppUser.GetUserForProfile;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using a = LawyerProject.Domain.Entities.Identity;

namespace LawyerProject.Application.Features.Queries.AppUser.GetUserDetailsByUserName
{
    public class GetUserDetailsByUserEmailQueryHandler : IRequestHandler<GetUserDetailsByUserEmailQueryRequest, GetUserDetailsByUserEmailQueryResponse>
    {
        private readonly IUserService _userService;

        public GetUserDetailsByUserEmailQueryHandler(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<GetUserDetailsByUserEmailQueryResponse> Handle(GetUserDetailsByUserEmailQueryRequest request, CancellationToken cancellationToken)
        {
            GetUserDetailsDto user =   await _userService.GetUserDetailsAsync(request.UserNameOrEmail);

            return new GetUserDetailsByUserEmailQueryResponse 
            {
                User = user,
            };

        }
    }
}
