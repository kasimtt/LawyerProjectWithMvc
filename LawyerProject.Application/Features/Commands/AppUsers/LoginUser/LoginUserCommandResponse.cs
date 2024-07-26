using LawyerProject.Application.DTOs.TokenDtos;
using LawyerProject.Application.DTOs.UserDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LawyerProject.Application.Features.Commands.AppUsers.LoginUser
{
    public class LoginUserCommandResponse
    {
        public Token Token { get; set; }
        public GetUserDto? User { get; set; }
    }

   
}
