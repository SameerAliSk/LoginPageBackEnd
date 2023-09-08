using LoginPage.API.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace LoginPage.API.Data
{
    public class UserLoginDbContext: DbContext
    {
        public UserLoginDbContext(DbContextOptions dbContextOptions) : base(dbContextOptions) 
        {
            
        }

        public DbSet<UserLogin> UserLogins { get; set; }
    }
}
