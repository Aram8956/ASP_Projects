using Project.DTO;

public interface IHotelServ
{
    Task<HotelReadDto> CreateAsync(HotelCreateDto dto);
    Task<IEnumerable<HotelReadDto>> GetAllAsync();
    Task<HotelReadDto?> GetByIdAsync(int id);
    Task UpdateAsync(int id, HotelUpdateDto dto);
    Task DeleteAsync(int id);
}
