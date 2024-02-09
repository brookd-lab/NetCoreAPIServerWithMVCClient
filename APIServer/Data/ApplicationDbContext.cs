using APIServer.Models;
using Microsoft.EntityFrameworkCore;

namespace APIServer.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            :base(options) { }

        public DbSet<Employee> Employee { get; set; }
    }
}
