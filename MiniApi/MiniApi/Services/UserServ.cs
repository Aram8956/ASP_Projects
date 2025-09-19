using Project.DTO;
using Project.Models;
using Project.Repositories;

namespace Project.Services
{
    public class UserServ : IUserServ
    {
        private readonly IUser _context;

        public UserServ(IUser userRepository)
        {
            _context = userRepository;
        }

        public async Task<UserReadDto> CreateAsync(UserCreateDto dto)
        {
            var user = new User
            {
                Name = dto.Username,
                Email = dto.Email
            };

            await _context.AddAsync(user);
            await _context.SaveChangesAsync();

            return new UserReadDto(
                user.Id,
                user.Name,
                user.Email,
                Profile: null!
            );
        }

        public async Task<UserReadDto?> GetByIdAsync(int id)
        {
            var user = await _context.GetUserWithProfileAsync(id);
            if (user == null) return null;

            return new UserReadDto(
                user.Id,
                user.Name,
                user.Email,
                user.Profile != null
                    ? new ProfileReadDto(
                        user.Profile.Id,
                        user.Profile.UserId,
                        user.Profile.Phone,
                        user.Profile.PassportNumber
                      )
                    : null!
            );
        }

        public async Task<List<UserReadDto>> GetAllAsync()
        {
            var users = await _context.GetAllAsync();
            return users.Select(u => new UserReadDto(
                u.Id,
                u.Name,
                u.Email,
                u.Profile != null
                    ? new ProfileReadDto(
                        u.Profile.Id,
                        u.Profile.UserId,
                        u.Profile.Phone,
                        u.Profile.PassportNumber
                      )
                    : null!
            )).ToList();
        }

        public async Task UpdateAsync(int id, UserUpdateDto dto)
        {
            var user = await _context.GetByIdAsync(id);
            if (user == null)
                throw new KeyNotFoundException("User not found");

            user.Name = dto.Username;
            user.Email = dto.Email;

            _context.Update(user);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var user = await _context.GetByIdAsync(id);
            if (user == null)
                throw new KeyNotFoundException("User not found");

            _context.Remove(user);
            await _context.SaveChangesAsync();
        }
    }
}
