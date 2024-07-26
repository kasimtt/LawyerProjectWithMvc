using LawyerProject.Application.Exceptions;
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

namespace LawyerProject.Application.Features.Queries.CalculationQuery.AnnualLeave.GetAnnualLeave
{
    public class GetAnnualLeaveQueryHandler : IRequestHandler<GetAnnualLeaveQueryRequest, GetAnnualLeaveQueryResponse>
    {
        public readonly INetToGroosReadRepository _netToGroosReadRepository;

        public GetAnnualLeaveQueryHandler(INetToGroosReadRepository netToGroosReadRepository)
        {
            _netToGroosReadRepository = netToGroosReadRepository;
        }

        public async Task<GetAnnualLeaveQueryResponse> Handle(GetAnnualLeaveQueryRequest request, CancellationToken cancellationToken)
        {
            var netToGross = await _netToGroosReadRepository.GetWhere(c => c.DataState == DataState.Active).FirstOrDefaultAsync();
            var dailyGrossFee = (request.NetSalary / netToGross.Coefficient) / 30;
            TimeSpan workingDays = DateTime.Now - request.DateOfEntry;

            GetAnnualLeaveQueryResponse response = new GetAnnualLeaveQueryResponse();

            if (workingDays.Days >= 365 && workingDays.Days < 1825)
                response.AnnualLeaveDay = 14;
            else
                if (workingDays.Days >= 1825 && workingDays.Days < 5475)
                response.AnnualLeaveDay = 24;
            else
                if (workingDays.Days >= 5475)
                response.AnnualLeaveDay = 26;
            else
                throw new LeaveTimeErrorException();

            response.AnnualFee = dailyGrossFee * response.AnnualLeaveDay;

            return response;
        }
    }
}
