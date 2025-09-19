using Project.DTO;
using Project.Models;
using Project.Repositories;

namespace Project.Services
{
    public class BookingServ : IBookingServ
    {
        private readonly IBooking _booking_context;
        private readonly IUser _user_context;
        private readonly IRoom _room_context;

        public BookingServ(IBooking bookingrep, IUser userrep, IRoom roomrep)
        {
            _booking_context = bookingrep;
            _user_context = userrep;
            _room_context = roomrep;
        }

        public async Task<BookingReadDto> CreateBookingAsync(BookingCreateDto dto)
        {
            var user = await _user_context.GetByIdAsync(dto.UserId);
            var room = await _room_context.GetByIdAsync(dto.RoomId);

            if (user == null)
            {
                throw new ArgumentException("Invalid UserId");
            }
            if (room == null)
            {
                throw new ArgumentException("Invalid RoomId");
            }
            if (dto.EndDate <= dto.StartDate) 
            { 
                throw new ArgumentException("Invalid datetime"); 
            }

            var days = (dto.EndDate - dto.StartDate).Days;
            if (days <= 0)
            {
                throw new ArgumentException("Invalid time input");
            }

            var booking = new Booking
            {
                UserId = dto.UserId,
                RoomId = dto.RoomId,
                CheckInDate = dto.StartDate,
                CheckOutDate = dto.EndDate,
                TotalPrice = days * room.Price
            };

            await _booking_context.AddAsync(booking);
            await _booking_context.SaveChangesAsync();

            return new BookingReadDto(
                booking.Id,
                booking.UserId,
                booking.RoomId,
                booking.CheckInDate,
                booking.CheckOutDate,
                booking.TotalPrice
            );
        }

        public async Task<List<BookingReadDto>> GetAllAsync()
        {
            var bookings = await _booking_context.GetAllAsync();

            return bookings.Select(b => new BookingReadDto(
                b.Id,
                b.UserId,
                b.RoomId,
                b.CheckInDate,
                b.CheckOutDate,
                b.TotalPrice
            )).ToList();
        }

        public async Task RemoveBookingAsync(int id)
        {
            var booking = await _booking_context.GetByIdAsync(id);
            if (booking == null) throw new ArgumentException("Booking not found");

            _booking_context.Remove(booking);
            await _booking_context.SaveChangesAsync();
        }
        
        public async Task<BookingReadDto?> GetByIdAsync(int id)
        {
            var booking = await _booking_context.GetBookingWithUserAndRoomAsync(id);
            if (booking == null) return null;

            return new BookingReadDto(
                booking.Id,
                booking.UserId,
                booking.RoomId,
                booking.CheckInDate,
                booking.CheckOutDate,
                booking.TotalPrice
            );
        }
    }
}
