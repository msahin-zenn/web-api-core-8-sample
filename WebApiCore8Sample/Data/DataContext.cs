using Microsoft.EntityFrameworkCore;
using WebApiCore8Sample.Models;

namespace WebApiCore8Sample.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }

        public DbSet<Character> Characters => Set<Character>();
        public DbSet<ApiUser> ApiUsers => Set<ApiUser>();
    }
}
