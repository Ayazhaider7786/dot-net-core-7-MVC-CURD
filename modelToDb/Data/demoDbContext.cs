using Microsoft.EntityFrameworkCore;
using modelToDb.Models.Domain;

namespace modelToDb.Data
{
    public class demoDbContext : DbContext
    {
        public demoDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<EmployeeModel> Employees { get; set; }
    }
}
