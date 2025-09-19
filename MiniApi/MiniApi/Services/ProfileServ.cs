using Project.DTO;
using Project.Models;
using Project.Repositories;

namespace Project.Services
{
    public class ProfileServ : IProfileServ
    {
        private readonly IProfile _profile_context;
        private readonly IUser _user_context;

        public ProfileServ(IProfile profileRepository, IUser userRepository)
        {
            _profile_context = profileRepository;
            _user_context = userRepository;
        }

        public async Task<ProfileReadDto> CreateAsync(ProfileCreateDto dto)
        {
            var user = await _user_context.GetByIdAsync(dto.UserId);
            if (user == null)
            {
                throw new ArgumentException("Invalid UserId");
            }

            var existingProfile = await _profile_context.GetProfileByUserIdAsync(dto.UserId);
            if (existingProfile != null)
            {
                throw new ArgumentException("Profile already exists");
            }

            var profile = new Profile
            {
                UserId = dto.UserId,
                Phone = dto.Phone,
                PassportNumber = dto.PassportNumber
            };

            await _profile_context.AddAsync(profile);
            await _profile_context.SaveChangesAsync();

            return new ProfileReadDto(
                profile.Id,
                profile.UserId,
                profile.Phone,
                profile.PassportNumber
            );
        }

        public async Task<ProfileReadDto?> GetByIdAsync(int id)
        {
            var profile = await _profile_context.GetProfileWithUserAsync(id);
            if (profile == null) return null;

            return new ProfileReadDto(
                profile.Id,
                profile.UserId,
                profile.Phone,
                profile.PassportNumber
            );
        }

        public async Task<List<ProfileReadDto>> GetAllAsync()
        {
            var profiles = await _profile_context.GetAllAsync();
            return profiles.Select(p => new ProfileReadDto(
                p.Id,
                p.UserId,
                p.Phone,
                p.PassportNumber
            )).ToList();
        }

        public async Task UpdateAsync(int id, ProfileUpdateDto dto)
        {
            var profile = await _profile_context.GetByIdAsync(id);
            if (profile == null)
            {
                throw new KeyNotFoundException("Profile not found");
            }

            profile.Phone = dto.Phone;
            profile.PassportNumber = dto.PassportNumber;

            _profile_context.Update(profile);
            await _profile_context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var profile = await _profile_context.GetByIdAsync(id);
            if (profile == null)
            {
                throw new KeyNotFoundException("Profile not found");
            }

            _profile_context.Remove(profile);
            await _profile_context.SaveChangesAsync();
        }
    }
}
