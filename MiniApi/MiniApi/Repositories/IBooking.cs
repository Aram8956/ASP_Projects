using Project.Models;

namespace Project.Repositories
{
    public interface IBooking: IRepository<Booking>
    {
        Task<IEnumerable<Booking>> GetBookingsByUserIdAsync(int userId);
        Task<IEnumerable<Booking>> GetBookingsByRoomIdAsync(int roomId);
        Task<Booking?> GetBookingWithUserAndRoomAsync(int id);
    }
}
