using AutoMapper;
using LawyerProject.Application.Abstractions.Services;
using LawyerProject.Application.DTOs.UserDtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LawyerProject.Application.Features.Queries.AppUser.GetUserByUserName
{
    public class GetUserByUserNameQueryHandler : IRequestHandler<GetUserByUserNameQueryRequest, GetUserByUserNameQueryResponse>
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public GetUserByUserNameQueryHandler(IUserService userService, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }


        public async  Task<GetUserByUserNameQueryResponse> Handle(GetUserByUserNameQueryRequest request, CancellationToken cancellationToken)
        {

            GetUserDto getUserDto =   await _userService.GetUserByUserNameAsync(request.UserNameOrEmail);
            GetUserByUserNameQueryResponse response = _mapper.Map<GetUserByUserNameQueryResponse>(getUserDto);

            return response;
        }
    }
}
