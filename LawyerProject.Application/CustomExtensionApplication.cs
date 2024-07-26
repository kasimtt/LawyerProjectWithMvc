using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LawyerProject.Application
{
    public static class CustomExtensionApplication
    {
        public static void AddContainerWithDependenciesApplication(this IServiceCollection services)
        {
            
            services.AddMediatR(x => x.RegisterServicesFromAssemblies(typeof(CustomExtensionApplication).Assembly));

        }
    }
}
