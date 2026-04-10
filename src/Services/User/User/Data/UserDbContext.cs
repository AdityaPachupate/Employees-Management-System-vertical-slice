using Microsoft.EntityFrameworkCore;
using Users.API.Models;

namespace Users.API.Data
{
    public class UserDbContext : DbContext
    {

        public UserDbContext(DbContextOptions<UserDbContext> options)
                    : base(options)
        {
        }

        public DbSet<User> Users { get; set; }

    }
}
