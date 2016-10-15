using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data.Entity;
using DAL.Entities;

namespace DAL
{
    public class BookDbInitializer : CreateDatabaseIfNotExists<BookingContext> 
    {
        //DropCreateDatabaseAlways 
        protected override void Seed(BookingContext db)
        {
            Random r = new Random(DateTime.Now.Millisecond);

            Author author = new Author { FirstName = "Katherine", LastName = "Hague" };
            db.Authors.Add(author);
            Book book = new Book { Isbn = "978-1-4919-4026-6", BookTitle = "The Entrepreneur's Guide to Raising Your First Round", Publisher = "O'Reilly Media", Author = author, Pages = 174, Date = 2016 };
            db.Books.Add(book);
            for (var j = 1; j < 10; j++)
            {
                db.Keepers.Add(new Keeper { Date = DateTime.UtcNow.Date.AddDays(-j), Count = r.Next(50, 150), Book=book, BookId = book.Id });
            }
            author = new Author { FirstName = "Matt", LastName = "Neuburg" };
            db.Authors.Add(author);
            book = new Book { Isbn = "978-1-4919-7007-2", BookTitle = "iOS 10 Programming Fundamentals with Swift", Publisher = "O'Reilly Media", Author = author, Pages = 620, Date = 2016 };
            db.Books.Add(book);
            for (var j = 1; j < 20; j++)
            {
                int count = r.Next(10, 100);
                db.Keepers.Add(new Keeper { Date = DateTime.UtcNow.Date.AddDays(-j), Count = count, Book = book ,BookId=book.Id});
            }
            book = new Book { Isbn = "978-1-4919-7016-4", BookTitle = "Programming iOS 10", Publisher = "O'Reilly Media", Author = author, Pages = 800, Date = 2012 };
            db.Books.Add(book);
            for (var j = 1; j < 15; j++)
            {
                int count = r.Next(300, 500);
                db.Keepers.Add(new Keeper { Date = DateTime.UtcNow.Date.AddDays(-j), Count = count, Book = book, BookId = book.Id });
            }
            author = new Author { FirstName = "Joseph", LastName = "Albahari" };
            db.Authors.Add(author);
            db.Books.Add(new Book { Isbn = "978-0-596-51924-7", BookTitle = "LINQ Pocket Reference", Publisher = "O'Reilly Media", Author = author, Pages = 374, Date = 2008 });

       
            base.Seed(db);
        }
    }
}
//DropCreateDatabaseAlways   CreateDatabaseIfNotExists