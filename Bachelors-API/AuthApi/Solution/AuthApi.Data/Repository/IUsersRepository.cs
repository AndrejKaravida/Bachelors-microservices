using AuthApi.Domain.Entities;
using System.Threading.Tasks;

namespace AuthApi.Data.Repository
{
    public interface IUsersRepository
    {
        void Add<T>(T entity) where T : class;
        void Delete<T>(T entity) where T : class;
        Task<bool> SaveAll();
        User GetUser(string authId);
    }
}
