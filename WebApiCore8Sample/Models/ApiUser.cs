namespace WebApiCore8Sample.Models
{
    public class ApiUser
    {
        public int Id { get; set; }
        public string Username { get; set; } = "";
        public byte[] PasswordHash { get; set; } = [];
        public byte[] PasswordSalt { get; set; } = [];
        public ApiUserRole Role { get; set; } = ApiUserRole.ReadOnly;
        public List<Character>? Characters { get; set; }
    }
}
