using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace DAL.Repository
{
   public class KeeperRepository : IRepository<Keeper>
    {
        private BookingContext db;

        public KeeperRepository(BookingContext context)
        {
            this.db = context;
        }

        public int Unique(int id,DateTime date)
        {
            Keeper keeper = (from x in db.Keepers where  x.BookId == id && x.Date == date  select x).FirstOrDefault();
            if (keeper != null) return keeper.Id;
            return -1;
        }

        public Keeper Get(int id)
        {
            return (from a in db.Keepers where a.Id == id select a).FirstOrDefault();
        }

        public void Create(Keeper keeper)
        {
            db.Keepers.Add(keeper);
        }


        public void Delete(int id)
        {
            var keeper = (from a in db.Keepers where a.Id == id select a).FirstOrDefault();
            if (keeper != null)
                db.Keepers.Remove(keeper);
        }

        public IEnumerable<Keeper> GetAll()
        {
            return db.Keepers;
        }

        public IEnumerable <Keeper> GetAvgKeepers(int? id)
        {
            IEnumerable<Keeper> avgKeepers = new List<Keeper>();
            if (id != null)
            {

                Book book = db.Books.Where(x => x.Id == id).FirstOrDefault();

                //avgKeepers  = (from bk in book.Keepers
                //              orderby bk.Date
                //              group bk by bk.Date into keep
                //              select new Keeper{ Date = keep.Key, Count = keep.Count(), Id = 1 }).ToList();

                avgKeepers = book.Keepers  //.SelectMany(b => b.Keepers)
                            .GroupBy(x => x.Date)
                            .Select(s => new Keeper
                            {
                                Id = 1,
                                Date = s.First().Date,
                               // Count = s.First().Count
                               Count=s.Sum(c=>c.Count)
                            })
                            .OrderBy(n=>n.Date)
                            .ToList();


            }
            return avgKeepers;
        }
        public void Update(Keeper item)
        {
            db.Entry(item).State = System.Data.Entity.EntityState.Modified;
        }
    }
}
