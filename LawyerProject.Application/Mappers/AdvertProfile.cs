using AutoMapper;
using LawyerProject.Application.DTOs.AdvertsDtos;
using LawyerProject.Application.Features.Commands.Adverts.CreateAdvert;
using LawyerProject.Application.Features.Commands.Adverts.UpdateAdvert;
using LawyerProject.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LawyerProject.Application.Mappers
{
    public class AdvertProfile : Profile
    {
        public AdvertProfile() 
        {
         
            CreateMap<Advert, CreateAdvertCommandRequest>().ReverseMap();
            CreateMap<Advert, UpdateAdvertCommandRequest>().ReverseMap();
            CreateMap<Advert, GetAdvertDto>().ReverseMap();
            CreateMap<Advert, GetAdvertDtoWithoutUser>().ReverseMap();
        
        }
    }
}
