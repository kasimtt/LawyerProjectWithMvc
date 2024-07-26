using LawyerProject.Application.Abstractions.Services;
using LawyerProject.Application.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppUSer = LawyerProject.Domain.Entities.Identity.AppUser;

namespace LawyerProject.Application.Features.Queries.AppUser.GetRolesToUserNameOrEmail
{
    public class GetRolesToUserNameOrEmailQueryHandler : IRequestHandler<GetRolesToUserNameOrEmailQueryRequest, GetRolesToUserNameOrEmailQueryResponse>
    {
        readonly private IUserService _userService;
        readonly private UserManager<AppUSer> _userManager;

        public GetRolesToUserNameOrEmailQueryHandler(IUserService userService, UserManager<AppUSer> userManager)
        {
            _userService = userService;
            _userManager = userManager;
        }

        public async Task<GetRolesToUserNameOrEmailQueryResponse> Handle(GetRolesToUserNameOrEmailQueryRequest request, CancellationToken cancellationToken)
        {
            AppUSer user = await _userManager.FindByEmailAsync(request.UserNameOrEmail);
            if (user == null)
            {
                user = await _userManager.FindByNameAsync(request.UserNameOrEmail);
            }//Kullanıcıyı bulduk. Şimdi rolünü çekicez

            var userRoles = await _userService.GetRolesToUserAsync(user.Id);
            return new()
            {
                UserRoles = userRoles,
            };
        }
    }
}
