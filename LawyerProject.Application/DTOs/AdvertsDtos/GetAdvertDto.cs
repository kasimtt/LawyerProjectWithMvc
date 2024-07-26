using LawyerProject.Application.DTOs.UserDtos;
using LawyerProject.Domain.Entities;
using LawyerProject.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LawyerProject.Application.DTOs.AdvertsDtos
{
    public class GetAdvertDto : BaseGetDto
    {
        
        public string UserFirstName { get; set; } = string.Empty;
        public string UserLastName { get; set; } = string.Empty;
        public string UserEmail { get; set; } = string.Empty;
        public string UserFullName { get; set; } = string.Empty;
        public string UserPhoneNumber { get; set; } = string.Empty;
        public string Avatar { get; set;  } = string.Empty;
        public int ObjectId { get; set; }
        public string CaseType { get; set; } = string.Empty;
        public DateTime CaseDate { get; set; }
        public decimal Price { get; set; }
        public string City { get; set; }
        public string Address { get; set; }
        public string District { get; set; }
        public string CasePlace { get; set; }
        public string Description { get; set; }

    }
}
