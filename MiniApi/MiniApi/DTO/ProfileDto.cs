namespace Project.DTO
{
    public record ProfileCreateDto(int UserId, string Phone, string PassportNumber);
    public record ProfileReadDto(int Id, int UserId, string Phone, string PassportNumber);
    public record ProfileUpdateDto(string Phone, string PassportNumber);
}
