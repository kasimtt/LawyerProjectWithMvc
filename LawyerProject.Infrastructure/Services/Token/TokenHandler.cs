using LawyerProject.Application.Abstractions.Token;
using T = LawyerProject.Application.DTOs.TokenDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Configuration;
using System.IdentityModel.Tokens.Jwt;
using System.Diagnostics;
using System.Security.Cryptography;
using LawyerProject.Domain.Entities.Identity;
using System.Security.Claims;

namespace LawyerProject.Infrastructure.Services.Token
{
    public class TokenHandler : ITokenHandler
    {
        private readonly IConfiguration _configuration;

        public TokenHandler(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public T.Token CreateAccessToken(int minute, AppUser user)
        {
            T.Token token = new();
            //securityKeyin simetriğini alıyoruz
            SymmetricSecurityKey securityKey = new(Encoding.UTF8.GetBytes(_configuration["Token:SecurityKey"]));
            //şifrelenmiş kimliği oluşturuyoruz
            SigningCredentials signingCredentials = new(securityKey, SecurityAlgorithms.HmacSha256);

            //oluşturulacak token ayarlarını veriyoruz

            token.Expiration = DateTime.UtcNow.AddMinutes(minute);

            var claims = new List<Claim>
            {
                  
                  new Claim(ClaimTypes.Email,user.Email),
                  new Claim(ClaimTypes.Name,user.FirstName),
                  new Claim(ClaimTypes.NameIdentifier,user.Id.ToString())
            };



            JwtSecurityToken tokenSecurityToken = new JwtSecurityToken(
                audience: _configuration["Token:Audience"],
                issuer: _configuration["Token:Issuer"],
                expires: token.Expiration,
                notBefore: DateTime.UtcNow,
                signingCredentials: signingCredentials,
                claims: claims 

                ) ;

            //token oluşturucu sınıfından bir token alalım
            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            token.AccessToken = tokenHandler.WriteToken(tokenSecurityToken);
           
            token.RefreshToken = CreateRefreshToken();
            return token;
        }

        public string CreateRefreshToken()
        {
            byte[] number = new byte[32];
            using RandomNumberGenerator random = RandomNumberGenerator.Create();
            random.GetBytes(number);
            return Convert.ToBase64String(number);
        }
    }
}
