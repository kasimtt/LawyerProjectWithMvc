using LawyerProject.Application.DTOs.UserDtos;
using LawyerProject.Application.Features.Commands.AppUsers.CreateUser;
using LawyerProject.Application.Features.Commands.AppUsers.LoginUser;
using LawyerProject.Application.Features.Commands.AppUsers.PasswordReset;
using MediatR;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Security.Claims;

namespace LawyerProject.MVC.Controllers
{
    public class AuthController : Controller
    {

        private readonly IMediator _mediator;

        public AuthController(IMediator mediator)
        {
            _mediator = mediator;
        }


        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]

        public async Task<IActionResult> Login(LoginUserCommandRequest request)
        {

            try
            {
                LoginUserCommandResponse response = await _mediator.Send(request);
                if (response.Token != null)
                {
                    var userClaims = new List<Claim>();


                    userClaims.Add(new Claim(ClaimTypes.NameIdentifier, response.User.Id ?? ""));
                    userClaims.Add(new Claim(ClaimTypes.Email, response.User.Email ?? ""));
                    userClaims.Add(new Claim(ClaimTypes.Name, response.User.UserName ??  ""));




                    var claimsIdentity = new ClaimsIdentity(userClaims, CookieAuthenticationDefaults.AuthenticationScheme);
                    var authProperties = new AuthenticationProperties
                    {
                        IsPersistent = true
                    };

                   await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

                    await HttpContext.SignInAsync(
                        CookieAuthenticationDefaults.AuthenticationScheme,
                        new ClaimsPrincipal(claimsIdentity),
                        authProperties);

                    

                    var User = HttpContext.User;

                    return RedirectToAction("Index", "Adverts");

                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Kullanıcı adı veya parola hatalı!");
            }


            return View(request);
        }


        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(CreateUserCommadRequest request)
        {
            try
            {
                CreateUserCommandResponse response = await _mediator.Send(request);
                if (response.Success)
                {
                    return RedirectToAction("Login", "Auth");
                }
                else
                {
                    ModelState.AddModelError("", response.Message);
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Kayıt oluşturulurken bir hata ile karşılaşıldı!");
            }

            return View();
        }




        [HttpPost]
        public async Task<IActionResult> PasswordReset(PasswordResetCommandRequest request)
        {
            PasswordResetCommandResponse response = await _mediator.Send(request);
            return View();
        }

    }








}
