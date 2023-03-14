using Microsoft.EntityFrameworkCore;
using UserApi.Models;

namespace UserApi.Data.Models
{
    public class UserDbContext : DbContext 
    {
        public UserDbContext(DbContextOptions<UserDbContext> options) : base(options) 
        { 
        
        }

        public DbSet<User> Users { get; set; }
    }
}
