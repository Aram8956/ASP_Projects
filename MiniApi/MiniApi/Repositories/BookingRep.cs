using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Models;

namespace Project.Repositories
{
    public class BookingRep: IBooking
    {
        private readonly AppDbContext _context;
        public BookingRep(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Booking>> GetAllAsync()
        {
            return await _context.Bookings.ToListAsync();
        }
        public async Task<Booking?> GetByIdAsync(int id)
        {
            return await _context.Bookings.FindAsync(id);
        }
        public async Task AddAsync(Booking entity)
        {
            await _context.Bookings.AddAsync(entity);
        }
        public void Update(Booking entity)
        {
            _context.Bookings.Update(entity);
        }
        public void Remove(Booking entity)
        {
            _context.Bookings.Remove(entity);
        }
        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
        public async Task<IEnumerable<Booking>> GetBookingsByUserIdAsync(int userId)
        {
            return await _context.Bookings
                .Where(b => b.UserId == userId)
                .ToListAsync();
        }
        public async Task<IEnumerable<Booking>> GetBookingsByRoomIdAsync(int roomId)
        {
            return await _context.Bookings
                .Where(b => b.RoomId == roomId)
                .ToListAsync();
        }
        public async Task<Booking?> GetBookingWithUserAndRoomAsync(int id)
        {
            return await _context.Bookings
                .Include(b => b.User)
                .Include(b => b.Room)
                .FirstOrDefaultAsync(b => b.Id == id);
        }
    }
}
