using Project.Models;
using Project.Data;
using Microsoft.EntityFrameworkCore;

namespace Project.Repositories
{
    public class HotelRep: IHotel
    {
        private readonly AppDbContext _context;
        public HotelRep(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Hotel entity)
        {
            await _context.Hotels.AddAsync(entity);
        }
        public async Task<IEnumerable<Hotel>> GetAllAsync()
        {
            return await _context.Hotels.ToListAsync();
        }
        public async Task<Hotel?> GetByIdAsync(int id)
        {
            return await _context.Hotels.FindAsync(id);
        }
        public async Task<Hotel?> GetHotelWithRoomsAsync(int id)
        {
            return await _context.Hotels
                .Include(h => h.Rooms)
                .FirstOrDefaultAsync(h => h.Id == id);
        }
        public void Remove(Hotel entity)
        {
            _context.Hotels.Remove(entity);
        }
        public void Update(Hotel entity)
        {
            _context.Hotels.Update(entity);
        }
        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
