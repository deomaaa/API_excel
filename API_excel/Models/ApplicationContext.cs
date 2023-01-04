using Microsoft.EntityFrameworkCore;

namespace API_excel.Models
{
    public class ApplicationContext : DbContext
    {
        public DbSet<Value> Values { get; set; } = null!;//Table Values
        public DbSet<Result> Results { get; set; } = null!;//Table Results
        public DbSet<FileItem> FileItems { get; set; } = null!;//Table FileItems

        public ApplicationContext(DbContextOptions<ApplicationContext> options)
            : base(options)
        {
            //Database.EnsureDeleted();
            Database.EnsureCreated();   //Creates a table on first access                           
        }
    }
}