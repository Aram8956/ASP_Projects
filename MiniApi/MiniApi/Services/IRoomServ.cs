using Project.DTO;
using Project.Models;

namespace Project.Services
{
    public interface IRoomServ
    {
        Task<Room> CreateAsync(RoomCreateDto dto);
        Task<IEnumerable<Room>> GetAllAsync();
        Task<IEnumerable<Room>> GetByHotelIdAsync(int hotelId);
        Task<Room?> GetByIdAsync(int id);
        Task UpdateAsync(int id, RoomUpdateDto dto);
        Task DeleteAsync(int id);
    }
}
