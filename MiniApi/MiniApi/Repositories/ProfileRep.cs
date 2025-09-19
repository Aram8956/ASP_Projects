using Project.Models;
using Project.Data;
using Microsoft.EntityFrameworkCore;

namespace Project.Repositories
{
    public class ProfileRep: IProfile
    {
        AppDbContext _context;
        public ProfileRep(AppDbContext context)
        {
            _context = context;
        }
        public Task AddAsync(Profile entity)
        {
            return _context.Profiles.AddAsync(entity).AsTask();
        }
        public async Task<IEnumerable<Profile>> GetAllAsync()
        {
            return await _context.Profiles.ToListAsync();
        }
        public async Task<Profile?> GetByIdAsync(int id)
        {
            return await _context.Profiles.FindAsync(id);
        }
        public void Update(Profile entity)
        {
            _context.Profiles.Update(entity);
        }
        public void Remove(Profile entity)
        {
            _context.Profiles.Remove(entity);
        }
        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
        public async Task<Profile?> GetProfileWithUserAsync(int id)
        {
            return await _context.Profiles
                .Include(p => p.User)
                .FirstOrDefaultAsync(p => p.Id == id);
        }
        public async Task<Profile?> GetProfileByUserIdAsync(int userId)
        {
            return await _context.Profiles
                .FirstOrDefaultAsync(p => p.UserId == userId);
        }
    }
}
