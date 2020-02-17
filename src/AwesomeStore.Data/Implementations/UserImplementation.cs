using System.Threading.Tasks;
using AwesomeStore.Data.Context;
using AwesomeStore.Data.Repository;
using AwesomeStore.Domain.Entities;
using AwesomeStore.Domain.Repository;
using Microsoft.EntityFrameworkCore;

namespace AwesomeStore.Data.Implementations
{
    public class UserImplementation : BaseRepository<UserEntity>, IUserRepository
    {
        private DbSet<UserEntity> _dataset;

        public UserImplementation(MyContext context): base(context)
        {
            _dataset = context.Set<UserEntity>();
        }

        public async Task<UserEntity> FindByEmail(string email)
        {
            return await _dataset.FirstOrDefaultAsync(u => u.Email.Equals(email));
        }
    }
}