using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace eftest
{
    public class EfTestDbContext : DbContext
    {
        private static LoggerFactory loggerFactory;

        static EfTestDbContext() {
            loggerFactory = new LoggerFactory();
            loggerFactory.AddProvider(new EFLoggingProvider());
        }

        public DbSet<Book> Books { get; set; }
        public DbSet<Author> Authors { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseInMemoryDatabase("testing");
            // optionsBuilder.UseSqlite("Filename=db.sqlite3");
            optionsBuilder.UseLoggerFactory(loggerFactory);
        }
    }
}