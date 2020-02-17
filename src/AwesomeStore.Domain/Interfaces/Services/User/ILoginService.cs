using System.Threading.Tasks;
using AwesomeStore.Domain.DTOs;

namespace AwesomeStore.Domain.Interfaces.Services.User
{
    public interface ILoginService
    {
         Task<object> FindByEmail(LoginDto user);
    }
}