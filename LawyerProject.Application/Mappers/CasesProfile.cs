using AutoMapper;
using LawyerProject.Application.DTOs.CasesDtos;
using LawyerProject.Application.Features.Commands.Case.UpdateCase;
using LawyerProject.Application.Features.Commands.CreateCase;
using LawyerProject.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LawyerProject.Application.Mappers
{
    public class CasesProfile : Profile
    {
        public CasesProfile() {
        
           CreateMap<Case,CreateCaseDto>().ReverseMap();
           CreateMap<Case,UpdateCaseDto>().ReverseMap();
           CreateMap<Case,GetCaseDto>().ReverseMap();
           CreateMap<Case,CreateCaseCommandRequest>().ReverseMap();
           CreateMap<Case,UpdateCaseCommandRequest>().ReverseMap();
          
        }
    }
}
