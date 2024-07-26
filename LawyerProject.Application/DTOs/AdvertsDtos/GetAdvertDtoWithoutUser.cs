using LawyerProject.Application.DTOs.UserDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LawyerProject.Application.DTOs.AdvertsDtos
{
    public class GetAdvertDtoWithoutUser: BaseGetDto
    {
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
