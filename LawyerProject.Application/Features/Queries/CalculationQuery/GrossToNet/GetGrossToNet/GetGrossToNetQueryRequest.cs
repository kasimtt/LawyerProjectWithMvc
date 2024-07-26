using MediatR;

namespace LawyerProject.Application.Features.Queries.CalculationQuery.GrossToNet.GetGrossToNet
{
    public class GetGrossToNetQueryRequest : IRequest<GetGrossToNetQueryResponse>
    {
        public double GrossFee { get; set; }
    }
}