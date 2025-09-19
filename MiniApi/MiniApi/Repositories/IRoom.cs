using Project.Models;

namespace Project.Repositories
{
    public interface IRoom: IRepository<Room>
    {
        Task<IEnumerable<Room>> GetRoomsByHotelIdAsync(int hotelId);
        Task<Room?> GetRoomWithHotelAsync(int id);
    }
}
