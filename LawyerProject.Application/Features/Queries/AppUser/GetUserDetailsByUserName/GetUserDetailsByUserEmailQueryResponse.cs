using LawyerProject.Application.DTOs.UserDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LawyerProject.Application.Features.Queries.AppUser.GetUserForProfile
{
    public class GetUserDetailsByUserEmailQueryResponse
    {
        public GetUserDetailsDto? User { get; set; }
    }
}
