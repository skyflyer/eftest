using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace eftest
{
    public class EfTestDbContext : DbContext
    {
        public DbSet<Book> Books { get; set; }
        public DbSet<Author> Authors { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseInMemoryDatabase("test");
            // optionsBuilder.UseSqlite("Filename=db.sqlite3");

            var lf = new LoggerFactory();
            lf.AddProvider(new EFLoggingProvider());
            // optionsBuilder.UseLoggerFactory(lf);
        }
    }
}