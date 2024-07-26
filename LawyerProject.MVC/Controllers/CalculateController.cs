using LawyerProject.Application.Features.Queries.CalculationQuery.AnnualLeave.GetAnnualLeave;
using LawyerProject.Application.Features.Queries.CalculationQuery.GrossToNet.GetGrossToNet;
using LawyerProject.Application.Features.Queries.CalculationQuery.NetToGross.GetNetToGross;
using LawyerProject.Application.Features.Queries.CalculationQuery.NoticePay.GetNoticePay;
using LawyerProject.Application.Features.Queries.CalculationQuery.SeverancePay.GetSeverancePay;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace LawyerProject.MVC.Controllers
{
    public class CalculateController : Controller
    {
        public IMediator _mediator;
        public CalculateController(IMediator mediator)
        {
            _mediator = mediator;
        }


        public IActionResult Index()
        {
            return View();
        }


        [HttpGet]
        public async Task<IActionResult> GetNetToGross( GetNetToGrossQueryRequest request)
        {
            GetNetToGrossQueryResponse response = await _mediator.Send(request);
            return Json(response);
        }

        [HttpGet]
        public async Task<IActionResult> GetGrossToNet(GetGrossToNetQueryRequest request)
        {
            GetGrossToNetQueryResponse response = await _mediator.Send(request);
            return Json(response);
        }

        [HttpGet]
        public async Task<IActionResult> GetSeverancePay( GetSeverancePayQueryRequest request)
        {
            GetSeverancePayQueryResponse response = await _mediator.Send(request);
            return Json(response);
        }

        [HttpGet]
        public async Task<IActionResult> GetNoticePay(GetNoticePayQueryRequest request)
        {
            GetNoticePayQueryResponse response = await _mediator.Send(request);
            return Json(response);
        }

        [HttpPost]
        public async Task<IActionResult> GetAnnualLeave([FromBody] GetAnnualLeaveQueryRequest request)
        {
            GetAnnualLeaveQueryResponse response = await _mediator.Send(request);
            return Ok(response);
        }


    }
}
