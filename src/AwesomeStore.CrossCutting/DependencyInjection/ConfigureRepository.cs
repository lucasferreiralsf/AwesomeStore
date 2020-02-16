using AwesomeStore.Data.Context;
using AwesomeStore.Data.Repository;
using AwesomeStore.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace AwesomeStore.CrossCutting.DependencyInjection
{
    public class ConfigureRepository
    {
        public static void ConfigureDependenciesRepository(IServiceCollection serviceCollection) {

            serviceCollection.AddScoped(typeof (IRepository<>), typeof(BaseRepository<>));
            serviceCollection.AddDbContext<MyContext>(options => options.UseMySql("Server=localhost;Port=3306;Database=awesome_store_db;Uid=root;Pwd=root"));
        }
    }
}