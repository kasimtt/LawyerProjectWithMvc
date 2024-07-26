using LawyerProject.Application.DTOs.AdvertsDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LawyerProject.Application.Features.Queries.Adverts.GetByIdAdvert
{
    public class GetByMyAdvertQueryResponse
    {
        public IEnumerable<GetAdvertDto?> Advert { get; set; }
    }
}
