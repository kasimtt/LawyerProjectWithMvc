using MediatR;

namespace LawyerProject.Application.Features.Queries.CalculationQuery.AnnualLeave.GetAnnualLeave
{
    public class GetAnnualLeaveQueryRequest : IRequest<GetAnnualLeaveQueryResponse>
    {
        public DateTime DateOfEntry { get; set; }
        public double NetSalary { get; set; }
    }
}