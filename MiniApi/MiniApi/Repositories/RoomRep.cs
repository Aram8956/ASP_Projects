using Project.Models;
using Project.Data;
using Microsoft.EntityFrameworkCore;

namespace Project.Repositories
{
    public class RoomRep: IRoom
    {
        private readonly AppDbContext _context;
        public RoomRep(AppDbContext context)
        {
            _context = context;
        }
        public async Task AddAsync(Room entity)
        {
            await _context.Rooms.AddAsync(entity);
        }
        public async Task<IEnumerable<Room>> GetAllAsync()
        {
            return await _context.Rooms.ToListAsync();
        }
        public async Task<Room?> GetByIdAsync(int id)
        {
            return await _context.Rooms.FindAsync(id);
        }
        public async Task<Room?> GetRoomWithHotelAsync(int id)
        {
            return await _context.Rooms
                .Include(r => r.Hotel)
                .FirstOrDefaultAsync(r => r.Id == id);
        }
        public async Task<IEnumerable<Room>> GetRoomsByHotelIdAsync(int hotelId)
        {
            return await _context.Rooms
                .Where(r => r.HotelId == hotelId)
                .ToListAsync();
        }
        public void Remove(Room entity)
        {
            _context.Rooms.Remove(entity);
        }
        public void Update(Room entity)
        {
            _context.Rooms.Update(entity);
        }
        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
