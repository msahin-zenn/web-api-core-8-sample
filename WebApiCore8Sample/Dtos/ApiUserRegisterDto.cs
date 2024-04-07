using WebApiCore8Sample.Models;

namespace WebApiCore8Sample.Dtos
{
    public class ApiUserRegisterDto
    {
        public string Username { get; set; } = "";
        public string Password { get; set; } = "";
        public ApiUserRole Role { get; set; } = ApiUserRole.ReadOnly;
    }
}
