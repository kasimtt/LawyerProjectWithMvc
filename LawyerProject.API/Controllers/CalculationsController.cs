using LawyerProject.Application.Features.Queries.CalculationQuery.AnnualLeave.GetAnnualLeave;
using LawyerProject.Application.Features.Queries.CalculationQuery.GrossToNet.GetGrossToNet;
using LawyerProject.Application.Features.Queries.CalculationQuery.NetToGross.GetNetToGross;
using LawyerProject.Application.Features.Queries.CalculationQuery.NoticePay.GetNoticePay;
using LawyerProject.Application.Features.Queries.CalculationQuery.SeverancePay.GetSeverancePay;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LawyerProject.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CalculationsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CalculationsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("getnettogross/{NetFee}")]
        public async Task<IActionResult> GetNetToGross([FromRoute] GetNetToGrossQueryRequest request)
        {
            GetNetToGrossQueryResponse response = await _mediator.Send(request);
            return Ok(response);
        }

        [HttpGet("getgrosstonet/{GrossFee}")]
        public async Task<IActionResult> GetGrossToNet([FromRoute] GetGrossToNetQueryRequest request)
        {
            GetGrossToNetQueryResponse response = await _mediator.Send(request);
            return Ok(response);
        }

        [HttpPost("getseverancepay")]
        public async Task<IActionResult> GetSeverancePay([FromBody] GetSeverancePayQueryRequest request)
        {
            GetSeverancePayQueryResponse response = await _mediator.Send(request);
            return Ok(response);
        }

        [HttpPost("getnoticepay")]
        public async Task<IActionResult> GetNoticePay([FromBody] GetNoticePayQueryRequest request)
        {
            GetNoticePayQueryResponse response = await _mediator.Send(request);
            return Ok(response);
        }

        [HttpPost("getannualleave")]
        public async Task<IActionResult> GetAnnualLeave([FromBody] GetAnnualLeaveQueryRequest request)
        {
            GetAnnualLeaveQueryResponse response = await _mediator.Send(request);
            return Ok(response);
        }
    }
}
