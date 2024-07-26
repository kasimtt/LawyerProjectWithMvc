using AutoMapper;
using LawyerProject.Application.DTOs.UserDtos;
using LawyerProject.Application.Features.Commands.Adverts.CreateAdvert;
using LawyerProject.Application.Features.Commands.AppUsers.CreateUser;
using LawyerProject.Application.Features.Queries.AppUser.GetUserByUserName;
using LawyerProject.Application.Features.Queries.Cases.GetByUserIdCase;
using LawyerProject.Domain.Entities;
using LawyerProject.Domain.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LawyerProject.Application.Mappers
{
    public class AppUserProfile: Profile
    {
        public AppUserProfile()
        {
            CreateMap<CreateUserCommadRequest, AppUser>().ReverseMap();
            CreateMap<CreateUserCommadRequest,CreateUserDto>().ReverseMap();
            CreateMap<CreateUserDto,AppUser>().ReverseMap();
            CreateMap<AppUser,GetUserDto>().ReverseMap();
            CreateMap<GetUserDto,GetUserByUserNameQueryResponse>().ReverseMap();
            CreateMap<GetUserDetailsDto,AppUser>().ReverseMap();
            
            // İki yönlü eşleme için ReverseMap() kullanmayı unutmayın
          

        }
    }
}
