using Project.DTO;

namespace Project.Services
{
    public interface IUserServ
    {
        Task<UserReadDto> CreateAsync(UserCreateDto dto);
        Task<UserReadDto?> GetByIdAsync(int id);
        Task<List<UserReadDto>> GetAllAsync();
        Task UpdateAsync(int id, UserUpdateDto dto);
        Task DeleteAsync(int id);
    }
}
