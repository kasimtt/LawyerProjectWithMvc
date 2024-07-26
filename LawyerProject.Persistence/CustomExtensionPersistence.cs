using LawyerProject.Application.Abstractions.Services;
using LawyerProject.Application.Abstractions.Services.Authentication;
using LawyerProject.Application.Repositories.AdvertRepositories;
using LawyerProject.Application.Repositories.CalculationRepositories.NetToGrossRepository;
using LawyerProject.Application.Repositories.CasePdfFileRepositories;
using LawyerProject.Application.Repositories.CaseRepositories;
using LawyerProject.Application.Repositories.EndpointRepositories;
using LawyerProject.Application.Repositories.FileRepositories;
using LawyerProject.Application.Repositories.MenuRepositories;
using LawyerProject.Application.Repositories.UserActivityRepositories;
using LawyerProject.Application.Repositories.UserImageFileRepositories;
using LawyerProject.Persistence.Repositories.AdvertRepositories;
using LawyerProject.Persistence.Repositories.CalculationRepositories.NetToGrossRepository;
using LawyerProject.Persistence.Repositories.CasePdfFileRepositories;
using LawyerProject.Persistence.Repositories.CaseRepositories;
using LawyerProject.Persistence.Repositories.EndpointRepositories;
using LawyerProject.Persistence.Repositories.FileRepositories;
using LawyerProject.Persistence.Repositories.MenuRepositories;
using LawyerProject.Persistence.Repositories.UserActivityRepositories;
using LawyerProject.Persistence.Repositories.UserImageFileRepositories;
using LawyerProject.Persistence.Services;
using Microsoft.Extensions.DependencyInjection;

namespace LawyerProject.Persistence
{
    public static class CustomExtensionPersistence
    {
        public static void AddContainerWithDependenciesPersistence(this IServiceCollection services)
        {

            services.AddScoped<IAdvertReadRepository, AdvertReadRepository>();
            services.AddScoped<IAdvertWriteRepository, AdvertWriteRepository>();

            services.AddScoped<IUserActivityReadRepository, UserActivityReadRepository>();
            services.AddScoped<IUserActivityWriteRepository, UserActivityWriteRepository>();

            services.AddScoped<ICaseReadRepository, CaseReadRepository>();
            services.AddScoped<ICaseWriteRepository, CaseWriteRepository>();

            services.AddScoped<IFileReadRepository, FileReadRepository>();
            services.AddScoped<IFileWriteRepository, FileWriteRepository>();

            services.AddScoped<ICasePdfFileReadRepository, CasePdfFileReadRepository>();
            services.AddScoped<ICasePdfFileWriteRepository, CasePdfFileWriteRepository>();

            services.AddScoped<IUserImageFileReadRepository, UserImageFileReadRepository>();
            services.AddScoped<IUserImageFileWriteRepository, UserImageFileWriteRepository>();

            services.AddScoped<IEndpointReadRepository, EndpointReadRepository>();
            services.AddScoped<IEndpointWriteRepository, EndpointWriteRepository>();

            services.AddScoped<IMenuReadRepository, MenuReadRepository>();
            services.AddScoped<IMenuWriteRepository, MenuWriteRepository>();

            services.AddScoped<INetToGroosReadRepository, NetToGroosReadRepository>();
            services.AddScoped<INetToGroosWriteRepository, NetToGroosWriteRepository>();




            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IInternalAuthentication, AuthService>();
            services.AddScoped<IExternalAuthentication, AuthService>();

            services.AddScoped<IRoleService, RoleService>();

            services.AddScoped<IAuthorizationEndpointService, AuthorizationEndpointService>();


        }
    }
}
