using Project.DTO;
using Project.Models;

namespace Project.Services
{
    public interface IBookingServ
    {
        Task<BookingReadDto> CreateBookingAsync(BookingCreateDto dto);
        Task<List<BookingReadDto>> GetAllAsync();
        Task<BookingReadDto?> GetByIdAsync(int id);
        Task RemoveBookingAsync(int id);
    }
}
