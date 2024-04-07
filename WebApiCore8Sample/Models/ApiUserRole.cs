using System.Text.Json.Serialization;

namespace WebApiCore8Sample.Models
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum ApiUserRole
    {
        ReadOnly = 1,
        User = 2,
        PowerUser = 3,
        Admin = 4,
        UserManagement = 5
    }
}
