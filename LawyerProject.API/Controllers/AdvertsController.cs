using LawyerProject.Application.Features.Commands.Adverts.CreateAdvert;
using LawyerProject.Application.Features.Commands.Adverts.DeleteAdvert;
using LawyerProject.Application.Features.Commands.Adverts.UpdateAdvert;
using LawyerProject.Application.Features.Queries.Adverts.GetAllAdvert;
using LawyerProject.Application.Features.Queries.Adverts.GetByIdAdvert;
using LawyerProject.Application.RequestParameters;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LawyerProject.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdvertsController : ControllerBase
    {
        public IMediator mediator;
        public AdvertsController(IMediator _mediator)
        {
            mediator = _mediator;
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> CreateAdvert([FromBody] CreateAdvertCommandRequest request)
        {
            CreateAdvertCommandResponse response = await mediator.Send(request);
            if (response.Success)
            {
                return Ok();
            }
            return BadRequest();
        }

        [HttpPut("[action]")]
        public async Task<IActionResult> UpdateAdvert([FromBody] UpdateAdvertCommandRequest request)
        {
            UpdateAdvertCommandResponse response = await mediator.Send(request);
            return Ok();
        }

        [HttpPut("[action]/{Id}")]
        public async Task<IActionResult> DeleteAdvert([FromRoute] DeleteAdvertCommandRequest request)
        {
            DeleteAdvertCommandResponse response = await mediator.Send(request);
            if (response.Success)
            {
                return Ok();
            }
            return BadRequest();

        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetAll([FromQuery] Pagination pagination)
        {
            GetAllAdvertQueryResponse response = await mediator.Send(new GetAllAdvertQueryRequest { Pagination = pagination });
            return Ok(response);
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetAllWithOutTotalCount([FromQuery] Pagination pagination)
        {
            GetAllAdvertQueryResponse response = await mediator.Send(new GetAllAdvertQueryRequest { Pagination = pagination });
            return Ok(response.Adverts);
        }

        [HttpGet("[action]/{UserNameOrEmail}")]
        public async Task<IActionResult> GetByMyAdvert([FromRoute] GetByMyAdvertQueryRequest request)
        {
            GetByMyAdvertQueryResponse response = await mediator.Send(request);

            return Ok(response.Advert);
        }


    }
}
