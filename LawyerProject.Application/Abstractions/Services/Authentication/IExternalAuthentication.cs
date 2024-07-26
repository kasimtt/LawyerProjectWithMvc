using T =  LawyerProject.Application.DTOs.TokenDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LawyerProject.Application.Abstractions.Services.Authentication
{
    public interface IExternalAuthentication  //Dış kaynaklardan gelen authentication işlemleri burada imzalanacak
    {
        Task<T.Token> GoogleLoginAsync(string idToken,string provider, int accessTokenLifeTime);
    }
}
