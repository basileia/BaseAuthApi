using BaseAuthApp_DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace BaseAuthApp_DAL.Data
{
    public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
    {        
        public DbSet<User> Users { get; set; }        
    }
}
