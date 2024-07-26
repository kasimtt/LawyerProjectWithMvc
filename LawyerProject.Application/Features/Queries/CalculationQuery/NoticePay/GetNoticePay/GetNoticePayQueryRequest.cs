using MediatR;

namespace LawyerProject.Application.Features.Queries.CalculationQuery.NoticePay.GetNoticePay
{
    public class GetNoticePayQueryRequest : IRequest<GetNoticePayQueryResponse>
    {
        public DateTime DateOfEntry { get; set; }
        public DateTime DateOfRelease { get; set; }
        public double NetSalary { get; set; }
    }
}