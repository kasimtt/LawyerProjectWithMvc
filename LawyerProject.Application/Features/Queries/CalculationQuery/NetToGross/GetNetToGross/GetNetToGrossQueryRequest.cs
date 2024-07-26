using MediatR;

namespace LawyerProject.Application.Features.Queries.CalculationQuery.NetToGross.GetNetToGross
{
    public class GetNetToGrossQueryRequest : IRequest<GetNetToGrossQueryResponse>
    {
        public double NetFee { get; set; }
    }
}