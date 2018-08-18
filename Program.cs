using System;
using System.Linq;

namespace eftest
{
    class Program
    {
        static void Main(string[] args)
        {
            Author existingAuthor;
            using (var ctx = new EfTestDbContext())
            {
                existingAuthor = new Author() { Name = "First author" };

                ctx.Add(existingAuthor);
                ctx.SaveChanges();
                Console.WriteLine($"Saved author ({existingAuthor.Name})");
            }

            using (var ctx = new EfTestDbContext())
            {
                var book1 = new Book()
                {
                    Name = "First book",
                    Author = existingAuthor
                };

                Console.WriteLine($"Added author ({existingAuthor.Name}) to first book. Author is existing: {ctx.Entry(existingAuthor).IsKeySet}");

                ctx.Add(book1);
                ctx.SaveChanges();
            }

            Console.WriteLine("ALL DONE");
        }
    }
}
