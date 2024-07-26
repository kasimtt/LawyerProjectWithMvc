using LawyerProject.Application.Repositories.CalculationRepositories.NetToGrossRepository;
using LawyerProject.Domain.Entities.Calculation;
using LawyerProject.Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LawyerProject.Application.Features.Queries.CalculationQuery.NoticePay.GetNoticePay
{
    public class GetNoticePayQueryHandler : IRequestHandler<GetNoticePayQueryRequest, GetNoticePayQueryResponse>
    {
        readonly private INetToGroosReadRepository _netToGroosReadRepository;

        public GetNoticePayQueryHandler(INetToGroosReadRepository netToGroosReadRepository)
        {
            _netToGroosReadRepository = netToGroosReadRepository;
        }

        public int numberOfNoticeDays { get; set; }

        public async Task<GetNoticePayQueryResponse> Handle(GetNoticePayQueryRequest request, CancellationToken cancellationToken)
        {
            // await _netToGroosReadRepository.GetWhere(c => c.DataState == DataState.Active).FirstOrDefaultAsync();
            var netToGross = 1.18; 
            var dailyGrossFee = (request.NetSalary / 1.18) ;

            TimeSpan workingDays = request.DateOfRelease - request.DateOfEntry;

            if (workingDays.Days < 180)
                numberOfNoticeDays = 14;
            else
                if (workingDays.Days >= 180 && workingDays.Days < 547)
                numberOfNoticeDays = 28;
            else
                if (workingDays.Days >= 547.5 && workingDays.Days < 1095)
                numberOfNoticeDays = 42;
            else
                if (workingDays.Days >= 1095)
                numberOfNoticeDays = 56;


            return new()
            {
                NoticePay = dailyGrossFee * numberOfNoticeDays
            };
        }
    }
}
