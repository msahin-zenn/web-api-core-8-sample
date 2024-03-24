namespace WebApiCore8Sample.Models
{
    public class ApiUsers
    {
        public static List<ApiUser> Users = new()
        {
            new ApiUser { Id = 1, Username= "Ahmet", Password= "1234", Role="Admin" },
            new ApiUser { Id = 2, Username= "Mehmet", Password= "1234", Role="PowerUser" },
            new ApiUser { Id = 3, Username= "Ayşe", Password= "1234", Role="StandardUser" }
        };
    }
}
