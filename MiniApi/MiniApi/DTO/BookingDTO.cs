namespace Project.DTO
{
    public record BookingCreateDto(int UserId, int RoomId, DateTime StartDate, DateTime EndDate);
    public record BookingReadDto(int Id, int UserId, int RoomId, DateTime StartDate, DateTime EndDate, decimal TotalPrice);
    public record BookingUpdateDto(int UserId, int RoomId, DateTime StartDate, DateTime EndDate);
}
