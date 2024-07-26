using LawyerProject.Application.Repositories.CaseRepositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using C = LawyerProject.Domain.Entities;
namespace LawyerProject.Application.Features.Queries.CasePdfFiles.GetCasePdfFile
{
    public class GetCasePdfFileQueryHandler : IRequestHandler<GetCasePdfFileQueryRequest, List<GetCasePdfFileQueryResponse>>
    {
        private readonly ICaseReadRepository _caseReadRepository;
        private readonly IConfiguration _configuration;

        public GetCasePdfFileQueryHandler(ICaseReadRepository caseReadRepository, IConfiguration configuration)
        {
            _caseReadRepository = caseReadRepository;
            _configuration = configuration;
        }

        public async Task<List<GetCasePdfFileQueryResponse>> Handle(GetCasePdfFileQueryRequest request, CancellationToken cancellationToken)
        {
            C.Case? Case = await _caseReadRepository.Table.Include(c => c.CasePdfFiles).FirstOrDefaultAsync(c => c.ObjectId == request.Id);

            return Case?.CasePdfFiles?.Select(c => new GetCasePdfFileQueryResponse
            {
                Path = $"{_configuration["BaseStorageUrl"]}/{c.Path}",
                Id = c.ObjectId,
                FileName = c.FileName
            }).ToList();
        }
    }
}
