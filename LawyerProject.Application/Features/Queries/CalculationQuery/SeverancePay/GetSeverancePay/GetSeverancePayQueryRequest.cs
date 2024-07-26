using MediatR;

namespace LawyerProject.Application.Features.Queries.CalculationQuery.SeverancePay.GetSeverancePay
{
    public class GetSeverancePayQueryRequest : IRequest<GetSeverancePayQueryResponse>
    {
        public DateTime DateOfEntry { get; set; }
        public DateTime DateOfRelease { get; set; }
        public double NetSalary { get; set; }
    }
}