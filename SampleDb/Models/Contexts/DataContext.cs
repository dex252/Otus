using Microsoft.EntityFrameworkCore;
using SampleDb.Models.Entity.Otus;

namespace SampleDb.Models.Contexts
{
    public class DataContext : DbContext
    {
        public DbSet<Course> Courses { get; set; }
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }
    }
}
