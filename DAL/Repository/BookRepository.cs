using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repository
{
    public class BookRepository : IRepository<Book>
    {
        private BookingContext db;

        public BookRepository(BookingContext context)
        {
            this.db = context;
        }
        public void Create(Book book)
        {
            db.Books.Add(book);
        }
        public IEnumerable<Book> GetAll()
        {
            return db.Books;
        }

        public void Delete(int id)
        {
            Book book = db.Books.Find(id);
            if (book != null)
                db.Books.Remove(book);
        }

        public Book Get(int id)
        {
            // db.Books.Find(id);
            return  (from b in db.Books where b.Id == id select b).FirstOrDefault();
        }



        public void Update(Book book)
        {
            db.Entry(book).State = System.Data.Entity.EntityState.Modified;
        }
    }
}
