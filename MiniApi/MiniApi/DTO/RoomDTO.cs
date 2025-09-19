namespace Project.DTO
{
    public record RoomCreateDto(int HotelId, string RoomNumber, string Type, decimal Price, bool IsAvailable);
    public record RoomReadDto(int Id, int HotelId, string RoomNumber, string Type, decimal Price, bool IsAvailable);
    public record RoomUpdateDto(int HotelId, string RoomNumber, string Type, decimal Price, bool IsAvailable);
}
