using LawyerProject.Application.Repositories.CalculationRepositories.NetToGrossRepository;
using LawyerProject.Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LawyerProject.Application.Features.Queries.CalculationQuery.GrossToNet.GetGrossToNet
{
    public class GetGrossToNetQueryHandler : IRequestHandler<GetGrossToNetQueryRequest, GetGrossToNetQueryResponse>
    {
        readonly private INetToGroosReadRepository _netToGroosReadRepository;
        public GetGrossToNetQueryHandler(INetToGroosReadRepository netToGroosReadRepository)
        {
            _netToGroosReadRepository = netToGroosReadRepository;
        }

        public async Task<GetGrossToNetQueryResponse> Handle(GetGrossToNetQueryRequest request, CancellationToken cancellationToken)
        {
            //await _netToGroosReadRepository.GetWhere(c => c.DataState == DataState.Active).FirstOrDefaultAsync();
            var netToGross = 0.18;
            var raise = netToGross * request.GrossFee;
            return new()
            {
                NetFee = request.GrossFee - raise
            };
        }
    }
}
