using Project.DTO;
using Project.Models;

namespace Project.Services
{
    public interface IProfileServ
    {
        Task<ProfileReadDto> CreateAsync(ProfileCreateDto dto);
        Task<ProfileReadDto?> GetByIdAsync(int id);
        Task<List<ProfileReadDto>> GetAllAsync();
        Task UpdateAsync(int id, ProfileUpdateDto dto);
        Task DeleteAsync(int id);
    }
}
