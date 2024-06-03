using Microsoft.EntityFrameworkCore;
using WebApi_for_User.Models;

namespace WebApi_for_User.AppDBContext
{
    public class UserContext : DbContext
    {
        public UserContext(DbContextOptions<UserContext> options) : 
            base(options) { }

        public virtual DbSet<User> Users { get; set; }
       
    }
}
