using AwesomeStore.Domain.Interfaces.Services.User;
using AwesomeStore.Service.Services;
using Microsoft.Extensions.DependencyInjection;

namespace AwesomeStore.CrossCutting.DependencyInjection
{
    public class ConfigureService
    {
        public static void ConfigureDependenciesService(IServiceCollection serviceCollection)
        {
            serviceCollection.AddTransient<IUserService, UserService>();
        }
    }
}