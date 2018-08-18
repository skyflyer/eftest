using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace eftest
{
    class Program
    {
        static void Main(string[] args)
        {
            Author existingAuthor;
            using (var ctx = new EfTestDbContext())
            {
                Console.WriteLine($"Setup:");
                var author = new Author { Name = "An author" };

                Console.WriteLine($"  Adding author ({author.Name}) to DB. Author's `IsKeySet`: {ctx.Entry(author).IsKeySet}");
                ctx.Add(author);
                ctx.SaveChanges();
                Console.WriteLine($"  Saved author ({author.Name})");

                Console.WriteLine($"  Retrieving author from DB");
                existingAuthor = ctx.Authors.First();
            }

            using (var ctx = new EfTestDbContext())
            {
                Console.WriteLine($"Adding new book and new author:");
                var book = new Book()
                {
                    Name = "First book",
                    Author = new Author { Name = "Another author" }
                };

                Console.WriteLine($"  Adding a new book with a new author to DB. Book's `IsKeySet`: {ctx.Entry(book).IsKeySet}");
                ctx.Add(book);
                ctx.SaveChanges();
                Console.WriteLine($"  Saved");
            }

            PrintDatabaseContents();

            using (var ctx = new EfTestDbContext())
            {
                Console.WriteLine($"Changing author of the book");
                var bookToChange = ctx.Books.First();
                bookToChange.AuthorID = existingAuthor.ID;
                Console.WriteLine($"  Changed book: {bookToChange.ID} - {bookToChange.Name} written by {bookToChange.AuthorID}");

                ctx.SaveChanges();
                Console.WriteLine($"  Saved changes");
            }

            using (var ctx = new EfTestDbContext())
            {
                Console.WriteLine($"Adding a new book with an existing author");
                var book = new Book()
                {
                    Name = "Second book",
                    AuthorID = existingAuthor.ID
                };

                Console.WriteLine($"  Added author ({existingAuthor.Name}) to a new book. Author's `IsKeySet`: {ctx.Entry(existingAuthor).IsKeySet}");

                ctx.Add(book);
                ctx.SaveChanges();
            }

            PrintDatabaseContents();

            Console.WriteLine("ALL DONE");
        }

        static void PrintDatabaseContents()
        {
            Console.WriteLine($"Database contents:");
            using (var ctx = new EfTestDbContext())
            {
                Console.WriteLine($"Authors:");
                foreach(var a in ctx.Authors) {
                    Console.WriteLine($"  {a.ID} - {a.Name}");
                }

                Console.WriteLine($"Books & authors:");
                foreach(var b in ctx.Books.Include(x => x.Author)) {
                    Console.WriteLine($"  {b.ID} - {b.Name} written by {b.Author.Name}");
                }
                Console.WriteLine($"");
            }

        }
    }
}
