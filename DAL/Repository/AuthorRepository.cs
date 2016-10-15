using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DAL.Repository
{
    public class AuthorRepository : IRepository<Author>
    {
        private BookingContext db;

        public AuthorRepository(BookingContext context)
        {
            this.db = context;
        }

        public void Create(Author author)
        {
            db.Authors.Add(author);
        }

        public void Delete(int id)
        {
            var author = (from a in db.Authors where a.Id == id select a).FirstOrDefault();
            if (author != null)
                db.Authors.Remove(author);
        }

        public Author Get(int id)
        {
            return (from a in db.Authors where a.Id == id select a).FirstOrDefault();
        }

        public IEnumerable<Author> GetAll()
        {
            return db.Authors;
        }

        public void Update(Author author)
        {
            db.Entry(author).State = System.Data.Entity.EntityState.Modified;
        }
    }
}
