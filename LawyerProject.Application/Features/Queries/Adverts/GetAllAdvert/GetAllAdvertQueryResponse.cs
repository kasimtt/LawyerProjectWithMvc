using LawyerProject.Application.DTOs.AdvertsDtos;
using LawyerProject.Domain.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LawyerProject.Application.Features.Queries.Adverts.GetAllAdvert
{
    public class GetAllAdvertQueryResponse
    {
        public int TotalCount { get; set; }
        public IEnumerable<GetAdvertDto>? Adverts { get; set; }
    }
}
