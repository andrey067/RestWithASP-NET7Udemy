using Domain.Entities;
using Domain.Repository;
using Domain.Shared.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {        
        private readonly DbSet<User> _dbSetUser;
        private readonly ICryptography _cryptography;

        public UserRepository(IRepository<User> repository, ICryptography cryptography)
        {            
            _dbSetUser = repository.GetDbSet();
            _cryptography = cryptography;
        }

        //TODO implementar Salvar usuario
        public User SaveUser(User user)
        {

            return null;
        }

        public async Task<(bool, User?)> ValidateCredential(string userName, string userPassword)
        {
            var userEntity = await _dbSetUser.FirstOrDefaultAsync(x => x.UserName == userName);

            if (userEntity == null) return (false, null);

            var verifyHashedEquals = _cryptography.VerifyHashedPassword(userEntity.Password, userPassword);

            return (verifyHashedEquals, userEntity);
        }      

        public async Task<User?> GetUserByUserName(string userName)
         => await _dbSetUser.SingleOrDefaultAsync(x => x.UserName == userName);
    }
}
