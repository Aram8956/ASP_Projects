using Project.Models;

namespace Project.Repositories
{
    public interface IProfile: IRepository<Profile>
    {
        Task<Profile?> GetProfileWithUserAsync(int id);
        Task<Profile?> GetProfileByUserIdAsync(int userId);
    }
}
