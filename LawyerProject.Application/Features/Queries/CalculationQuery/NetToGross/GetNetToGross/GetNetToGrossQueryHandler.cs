using LawyerProject.Application.Repositories.CalculationRepositories.NetToGrossRepository;
using LawyerProject.Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LawyerProject.Application.Features.Queries.CalculationQuery.NetToGross.GetNetToGross
{
    public class GetNetToGrossQueryHandler : IRequestHandler<GetNetToGrossQueryRequest, GetNetToGrossQueryResponse>
    {
        private readonly INetToGroosReadRepository _netToGroosReadRepository;

        public GetNetToGrossQueryHandler(INetToGroosReadRepository netToGroosReadRepository)
        {
            _netToGroosReadRepository = netToGroosReadRepository;
        }

        public async Task<GetNetToGrossQueryResponse> Handle(GetNetToGrossQueryRequest request, CancellationToken cancellationToken)
        {
            await _netToGroosReadRepository.GetWhere(c => c.DataState == DataState.Active).FirstOrDefaultAsync();
            var netToGross = 1.18;
            return new()
            { 
                GrossFee = request.NetFee * netToGross  // değiştirilmiştir
            };
        }
    }
}
