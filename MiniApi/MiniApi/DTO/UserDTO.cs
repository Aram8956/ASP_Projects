namespace Project.DTO
{
    public record UserCreateDto(string Username, string Email);
    public record UserReadDto(int Id, string Username, string Email, ProfileReadDto Profile);
    public record UserUpdateDto(string Username, string Email);
}
