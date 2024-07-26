using Google.Apis.Auth;
using Google.Apis.Auth.OAuth2;
using LawyerProject.Application.Abstractions.Services;
using LawyerProject.Application.Abstractions.Services.Authentication;
using LawyerProject.Application.Abstractions.Token;
using LawyerProject.Application.DTOs.TokenDtos;
using LawyerProject.Application.Exceptions;
using LawyerProject.Application.Features.Commands.AppUsers.LoginUser;
using LawyerProject.Application.Helpers;
using LawyerProject.Domain.Entities.Identity;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace LawyerProject.Persistence.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly ITokenHandler _tokenHandler;
        private readonly IConfiguration _configuration;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly IUserService _userService;
        private readonly IMailService _mailService;

        public AuthService(UserManager<AppUser> userManager, ITokenHandler tokenHandler, IConfiguration configuration, SignInManager<AppUser> signInManager, IUserService userService, IMailService mailService)
        {
            _userManager = userManager;
            _tokenHandler = tokenHandler;
            _configuration = configuration;
            _signInManager = signInManager;
            _userService = userService;
            _mailService = mailService;
        }


        async Task<Token> CreateUserExternalAsync(AppUser user, string name, string email, string givenName, string familyName, UserLoginInfo info, int accessTokenLifeTime)
        {
            bool result = user != null;
            if (user == null)
            {
                user = await _userManager.FindByEmailAsync(email);
                if (user == null)
                {
                    user = new AppUser
                    {
                        Id = Guid.NewGuid().ToString(),
                        Email = email,
                        UserName = email,
                        FirstName = givenName,
                        LastName = familyName,
                    };
                    var IdentityResult = await _userManager.CreateAsync(user);
                    result = IdentityResult.Succeeded;
                }
            }
            if (result)
            {
                await _userManager.AddLoginAsync(user, info);  //AspNetUserlogine kaydedilecek
            }
            else
                throw new InvalidExternalAuthentication();

            Token token = _tokenHandler.CreateAccessToken(accessTokenLifeTime, user);
            await _userService.UpdateRefreshTokenAsync(token.RefreshToken, user, token.Expiration, 15);

            return token;


        }

        public async Task<Token> GoogleLoginAsync(string idToken, string provider, int accessTokenLifeTime)
        {
            var setting = new GoogleJsonWebSignature.ValidationSettings
            {
                Audience = new List<string> { _configuration["ExternalLoginSetting:Google:Client_Id"] }
            };

            var payLoad = await GoogleJsonWebSignature.ValidateAsync(idToken, setting);
            var info = new UserLoginInfo(provider, payLoad.Subject, provider);
            AppUser user = await _userManager.FindByLoginAsync(info.LoginProvider, info.ProviderKey);
            Token token = await CreateUserExternalAsync(user, payLoad.Name, payLoad.Email, payLoad.GivenName, payLoad.FamilyName, info, accessTokenLifeTime);

            return token;

        }

        public async Task<Token> LoginAsync(string userNameOrEmail, string password, int accessTokenLifeTime)
        {
            AppUser user = await _userManager.FindByNameAsync(userNameOrEmail);
            if (user == null)
                user = await _userManager.FindByEmailAsync(userNameOrEmail);
            if (user == null)
                throw new NotFoundUserException();

            SignInResult result = await _signInManager.CheckPasswordSignInAsync(user, password, false);

            if (result.Succeeded) //authentication başarılı
            {
                //Token token = _tokenHandler.CreateAccessToken(accessTokenLifeTime, user);
               // await _userService.UpdateRefreshTokenAsync(token.RefreshToken, user, token.Expiration, 15);
                return new()
                {
                    AccessToken = Guid.NewGuid().ToString(),
                    RefreshToken = Guid.NewGuid().ToString(),
                    Expiration = DateTime.UtcNow.AddMinutes(accessTokenLifeTime)

                };
            }
            throw new AuthenticationErrorException();
        }

        public async Task<Token> RefreshTokenLoginAsync(string refreshToken)
        {
            AppUser? user = await _userManager.Users.FirstOrDefaultAsync(u => u.RefreshToken == refreshToken);
            if (user != null && user?.RefreshTokenEndDate > DateTime.UtcNow)
            {
                Token token = _tokenHandler.CreateAccessToken(15, user);
                await _userService.UpdateRefreshTokenAsync(token.RefreshToken, user, token.Expiration, 15);
                return token;
            }
            else
                throw new NotFoundUserException();

        }

        public async Task PasswordResetAsync(string email)
        {
            AppUser user = await _userManager.FindByEmailAsync(email);
            if (user != null)
            {
                string resetToken = await _userManager.GeneratePasswordResetTokenAsync(user);
                //byte[] tokenBytes = Encoding.UTF8.GetBytes(resetToken);
                //resetToken = WebEncoders.Base64UrlEncode(tokenBytes);
                resetToken = resetToken.UrlEncode();
                await _mailService.SendPasswordResetMailAsync(email, user.Id, resetToken);
            }
        }

        public async Task<bool> VerifyResetTokenAsync(string resetToken, string userId)
        {
            AppUser user = await _userManager.FindByIdAsync(userId);
            if (user != null)
            {
                //byte[] tokenBytes = WebEncoders.Base64UrlDecode(resetToken);
                //resetToken = Encoding.UTF8.GetString(tokenBytes);
                resetToken = resetToken.UrlDecode();
                return await _userManager.VerifyUserTokenAsync(user, _userManager.Options.Tokens.PasswordResetTokenProvider, "ResetPassword", resetToken);
            }
            return false;
        }
    }
}
