using System.Threading.Tasks;
using AwesomeStore.Domain.Entities;
using AwesomeStore.Domain.Interfaces;

namespace AwesomeStore.Domain.Repository
{
    public interface IUserRepository: IRepository<UserEntity>
    {
         Task<UserEntity> FindByEmail(string email);
    }
}