namespace Project.Models
{
    public class Profile
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Phone { get; set; }
        public string PassportNumber { get; set; }

        public User User { get; set; }
    }
}
