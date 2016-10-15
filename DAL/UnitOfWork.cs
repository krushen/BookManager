using DAL.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
   public class UnitOfWork : IDisposable
    {
        private BookingContext db = new BookingContext();
        private AuthorRepository authorRepository;
        private BookRepository bookRepository;
        private KeeperRepository keeperBookRepository;
        public BookRepository Books
        {
            get
            {
                if (bookRepository == null)
                    bookRepository = new BookRepository(db);
                return bookRepository;
            }
            
        }

        public AuthorRepository Authors
        {
            get
            {
                if (authorRepository == null)
                    authorRepository = new AuthorRepository(db);
                return authorRepository;
            }
        }

        public KeeperRepository Keepers
        {
            get
            {
                if (keeperBookRepository == null)
                    keeperBookRepository = new KeeperRepository(db);
                return keeperBookRepository;
            }

        }

        public void Save()
        {
            db.SaveChanges();
        }
        private bool disposed = false;

        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    db.Dispose();
                }
                this.disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
