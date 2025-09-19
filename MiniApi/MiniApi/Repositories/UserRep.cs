using Project.Models;
using Microsoft.EntityFrameworkCore;
using Project.Data;

namespace Project.Repositories
{
    public class UserRep: IUser
    {
        private readonly AppDbContext _context;
        public UserRep(AppDbContext context)
        {
            _context = context;
        }
        
        public Task AddAsync(User entity)
        {
            return _context.Users.AddAsync(entity).AsTask();
        }
        public async Task<IEnumerable<User>> GetAllAsync()
        {
            return await _context.Users.ToListAsync();
        }
        public async Task<User?> GetByIdAsync(int id)
        {
            return await _context.Users.FindAsync(id);
        }
        public void Update(User entity)
        {
            _context.Users.Update(entity);
        }
        public void Remove(User entity)
        {
            _context.Users.Remove(entity);
        }
        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
        public async Task<User?> GetUserWithProfileAsync(int id)
        {
            return await _context.Users
                .Include(u => u.Profile)
                .FirstOrDefaultAsync(u => u.Id == id);
        }
    }
}
