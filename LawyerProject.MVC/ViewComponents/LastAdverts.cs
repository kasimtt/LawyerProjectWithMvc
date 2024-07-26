using LawyerProject.Application.Features.Queries.Adverts.GetAllAdvert;
using LawyerProject.Application.RequestParameters;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace LawyerProject.MVC.ViewComponents
{
    public class LastAdverts: ViewComponent
    {
        public IMediator mediator;
        public LastAdverts(IMediator _mediator)
        {
            mediator = _mediator;
        }
        
        public async Task<IViewComponentResult> InvokeAsync(Pagination pagination)
        {
            GetAllAdvertQueryResponse response = await mediator.Send(new GetAllAdvertQueryRequest { Pagination = pagination });

            return View(response);
        }

    }
}
