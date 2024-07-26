using AutoMapper;
using LawyerProject.Application.Repositories.UserActivityRepositories;
using LawyerProject.Domain.Entities;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;

namespace LawyerProject.API.Extensions
{
    public class UserActivity_ : IActionFilter
    {
        private readonly IUserActivityWriteRepository _userActivityWriteRepository;
     
        public UserActivity_(IUserActivityWriteRepository userActivityWriteRepository)
        {
            _userActivityWriteRepository = userActivityWriteRepository;
          
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            //throw new NotImplementedException();
        }

        public async void OnActionExecuting(ActionExecutingContext context)
        {
            var data = "";
            var path = context.HttpContext.Request.Path;
            var ipAddress = context.HttpContext.Connection.RemoteIpAddress.ToString();
            var userName = context.HttpContext.User.Identity.IsAuthenticated ? context.HttpContext.User.Identity.Name : "Guest";

            if (!string.IsNullOrEmpty(context.HttpContext.Request.QueryString.Value))
            {
                data = context.HttpContext.Request.QueryString.Value;
            }
            else
            {
                var dataModel = context.ActionArguments.FirstOrDefault();
                data = JsonConvert.SerializeObject(dataModel);
            }

            #region Create Model & Save DB
            var userActiviy = new UserActivity
            {
                Data = data,
                IpAdresi = ipAddress,
                KullaniciId = userName,
                Tarih = DateTime.Now,
                Path = path,
                CreatedDate = DateTime.Now,
                DataState = Domain.Enums.DataState.Active,

            };

            await _userActivityWriteRepository.AddAsync(userActiviy);
            _userActivityWriteRepository.Save();
            #endregion
        }
    }
}
