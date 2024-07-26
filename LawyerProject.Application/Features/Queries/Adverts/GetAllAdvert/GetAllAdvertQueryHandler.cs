using AutoMapper;
using LawyerProject.Application.DTOs.AdvertsDtos;
using LawyerProject.Application.DTOs.CasesDtos;
using LawyerProject.Application.Features.Queries.Cases.GetAllCase;
using LawyerProject.Application.Repositories.AdvertRepositories;
using LawyerProject.Domain.Entities;
using LawyerProject.Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LawyerProject.Application.Features.Queries.Adverts.GetAllAdvert
{
    public class GetAllAdvertQueryHandler : IRequestHandler<GetAllAdvertQueryRequest, GetAllAdvertQueryResponse>
    {
        private readonly IAdvertReadRepository _advertReadRepository;
        private readonly IMapper _mapper;
        readonly ILogger<GetAllAdvertQueryHandler> _logger;

        public GetAllAdvertQueryHandler(IAdvertReadRepository advertReadRepository, IMapper mapper, ILogger<GetAllAdvertQueryHandler> logger)
        {
            _advertReadRepository = advertReadRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<GetAllAdvertQueryResponse> Handle(GetAllAdvertQueryRequest request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Bütün ilanlar listelendi");
            int totalCount = _advertReadRepository.GetAll(false).Count();
            var result = _advertReadRepository.Table.Include(i => i.User).Skip(request.Pagination.Page * request.Pagination.Size).Take(request.Pagination.Size).Where(c => c.DataState == DataState.Active).ToList();

            IEnumerable<GetAdvertDto> entityDto = _mapper.Map<IEnumerable<Advert>, IEnumerable<GetAdvertDto>>(result).ToList();

            return new GetAllAdvertQueryResponse
            {
                Adverts = entityDto,
                TotalCount = totalCount,
            };


        }
    }


}
