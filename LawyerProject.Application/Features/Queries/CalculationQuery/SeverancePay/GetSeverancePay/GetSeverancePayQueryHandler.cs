using LawyerProject.Application.Repositories.CalculationRepositories.NetToGrossRepository;
using LawyerProject.Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LawyerProject.Application.Features.Queries.CalculationQuery.SeverancePay.GetSeverancePay
{
    public class GetSeverancePayQueryHandler : IRequestHandler<GetSeverancePayQueryRequest, GetSeverancePayQueryResponse>
    {
        readonly private INetToGroosReadRepository _netToGroosReadRepository;

        public GetSeverancePayQueryHandler(INetToGroosReadRepository netToGroosReadRepository)
        {
            _netToGroosReadRepository = netToGroosReadRepository;
        }

        public async Task<GetSeverancePayQueryResponse> Handle(GetSeverancePayQueryRequest request, CancellationToken cancellationToken)
        {
            //await _netToGroosReadRepository.GetWhere(c => c.DataState == DataState.Active).FirstOrDefaultAsync();
            var netToGross = 1.18;
            TimeSpan daysOfSeniority = request.DateOfRelease - request.DateOfEntry;

            if (daysOfSeniority.Days < 365)
            {
                throw new InsufficientExecutionStackException();
            }

            var dailyGrossFee = (request.NetSalary * netToGross) ;  // bunlar değiştirildi

            return new()
            {
                SeverancePay = (dailyGrossFee * daysOfSeniority.Days * 30) / 365
            };
        }
    }
}
