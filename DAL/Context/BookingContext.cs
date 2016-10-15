namespace DAL
{
    using System;
    using System.Data.Entity;
    using System.Linq;
    using DAL.Entities;



    public class BookingContext : DbContext
    {
       
        public BookingContext()
            : base("name=BookingContext")
        {
           Database.SetInitializer(new BookDbInitializer());
            // Database.SetInitializer<BookingContext>(new CreateDatabaseIfNotExists<BookingContext>());
            //Database.SetInitializer<SchoolDBContext>(new DropCreateDatabaseIfModelChanges<SchoolDBContext>());
            //Database.SetInitializer<SchoolDBContext>(new DropCreateDatabaseAlways<SchoolDBContext>());
            //Database.SetInitializer<SchoolDBContext>(new SchoolDBInitializer());
        }

        public virtual DbSet<Book> Books { get; set; }
        public virtual DbSet<Author> Authors { get; set; }
        public virtual DbSet<Keeper> Keepers { get; set; }

    }


}