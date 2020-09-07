using AuthApi.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using AuthApi.API.Data;

namespace AuthApi.Data.Repository
{
    public class UsersRepository : IUsersRepository
    {

        private readonly DataContext _context;

        public UsersRepository(DataContext context)
        {
            _context = context;
        }

        public void Add<T>(T entity) where T : class
        {
            _context.Add(entity);
        }

        public void Delete<T>(T entity) where T : class
        {
            _context.Remove(entity);
        }

      
        public User GetUser(string authId)
        {
            return _context.Users.Where(x => x.AuthId == authId).Include(x => x.Role).FirstOrDefault();
        }

        public async Task<bool> SaveAll()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
