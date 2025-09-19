using  Project.Models;

namespace Project.Repositories
{
    public interface IUser: IRepository<User>
    {
        Task<User?> GetUserWithProfileAsync(int id);
    }
}
