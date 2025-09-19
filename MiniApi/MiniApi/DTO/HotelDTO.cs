namespace Project.DTO
{
   public record HotelCreateDto(string Name, string Address, string City, string Country, int Rating);
    public record HotelReadDto(int Id, string Name, string Address, string City, string Country, int Rating, IEnumerable<RoomReadDto> Rooms);
    public record HotelUpdateDto(string Name, string Address, string City, string Country, int Rating);
}
