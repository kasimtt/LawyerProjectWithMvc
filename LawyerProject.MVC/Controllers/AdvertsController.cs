using LawyerProject.Application.Features.Commands.Adverts.CreateAdvert;
using LawyerProject.Application.Features.Commands.Adverts.DeleteAdvert;
using LawyerProject.Application.Features.Queries.Adverts.GetAllAdvert;
using LawyerProject.Application.Features.Queries.Adverts.GetByIdAdvert;
using LawyerProject.Application.RequestParameters;
using LawyerProject.MVC.Models;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Security.Claims;

namespace LawyerProject.MVC.Controllers
{
    public class AdvertsController : Controller
    {
        public IMediator mediator;
        public AdvertsController(IMediator _mediator)
        {
            mediator = _mediator;
        }

        public async Task<IActionResult> Index([FromQuery] Pagination pagination)
        {
            GetAllAdvertQueryResponse response = await mediator.Send(new GetAllAdvertQueryRequest { Pagination = pagination });

            return View(response);
        }

       

        public IActionResult Privacy()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Post( CreateAdvertCommandRequest request)
        {
            var claims= User.Claims;
            string email = User.FindFirstValue(ClaimTypes.Email);
            request.UserNameOrEmail = "alp@gmail.com";

            CreateAdvertCommandResponse response = await mediator.Send(request);
            if (response.Success)
            {
                return View();
            }
            return View();
        }

  
        public async Task<IActionResult> GetByMyAdvert(GetByMyAdvertQueryRequest request)
        {
            request.UserNameOrEmail = "alp@gmail.com";
            GetByMyAdvertQueryResponse response = await mediator.Send(request);

            return View(response);
        }


        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            DeleteAdvertCommandResponse response = await mediator.Send(new DeleteAdvertCommandRequest { Id = id});
            if (response.Success)
            {
                return RedirectToAction("GetByMyAdvert");
            }
            return View();

        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
