using T = LawyerProject.Application.DTOs.TokenDtos;
namespace LawyerProject.Application.Abstractions.Services.Authentication
{
    public interface IInternalAuthentication  // İc kaynaktan gelen Authentication işlemleri burada imzalanacak
    {
        Task<T.Token> LoginAsync(string userNameOrEmail, string password, int accessTokenLifeTime);
        Task<T.Token> RefreshTokenLoginAsync(string refreshToken);
    }
}
