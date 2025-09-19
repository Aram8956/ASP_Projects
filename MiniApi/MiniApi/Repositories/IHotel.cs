using Project.Models;

namespace Project.Repositories
{
    public interface IHotel: IRepository<Hotel>
    { 
        Task<Hotel?> GetHotelWithRoomsAsync(int id);
    }
}
