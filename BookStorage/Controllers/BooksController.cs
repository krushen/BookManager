using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DAL;
using DAL.Entities;
using BookStorage.Models;
using Google.DataTable.Net.Wrapper;
namespace BooksStorage.Controllers
{
    public class BooksController : Controller
    {
        UnitOfWork unitOfWork;
        // GET: Books
        public BooksController()
        {
            unitOfWork = new UnitOfWork();
        }
        public ActionResult Index()
        {
          
            return View(unitOfWork.Books.GetAll());
        }

        // GET: Books/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }
            Book book = unitOfWork.Books.Get((int)id);

            if (book != null)
            {
                int keep = unitOfWork.Keepers.Unique(book.Id, DateTime.UtcNow.Date);
                if (keep==-1)
                {
                    Keeper keeper = new Keeper() { Date = DateTime.UtcNow.Date, Count = 1,Book=book, BookId = book.Id };
                    unitOfWork.Keepers.Create(keeper);
                    unitOfWork.Save();
                }
                else
                {
                    Keeper keeper = unitOfWork.Keepers.Get(keep);
                    keeper.Count += 1;
                    unitOfWork.Keepers.Update(keeper);
                    unitOfWork.Save();
                }
            }
            IEnumerable<Keeper> AvgKeepers = unitOfWork.Keepers.GetAvgKeepers((id));
            ViewBag.ChartData =GoogleChartData(AvgKeepers);
            return View(book);
        }
         string GoogleChartData(IEnumerable<Keeper> keepers)
        {
            var dt = new Google.DataTable.Net.Wrapper.DataTable();
            dt.AddColumn(new Column(ColumnType.Date, "Date", "Date"));
            dt.AddColumn(new Column(ColumnType.Number, "Count", "Count"));

            foreach (var item in keepers)
            {
                Google.DataTable.Net.Wrapper.Row row = dt.NewRow();
                row.AddCellRange(new Cell[]
                {
                     new Cell(item.Date),
                     new Cell(item.Count),
                });
                dt.AddRow(row);
            }
            return dt.GetJson();
        }
        // GET: Books/Create
        public ActionResult Create()
        {
            SelectList authors = new SelectList(unitOfWork.Authors.GetAll(), "Id", "Name");
            ViewBag.Authors = authors;
            return View();
        }

        // POST: Books/Create
        [HttpPost]
        public ActionResult Create(Book book)
        {
            if (ModelState.IsValid)
            {
                if (book.AuthorId != null)
                {
                    book.Author = unitOfWork.Authors.Get((int)book.AuthorId);
                    unitOfWork.Books.Create(book);
                    unitOfWork.Save();
                }
                return RedirectToAction("Index");
            }

            return View(book);
        }

        // GET: Books/Edit/5
        public ActionResult Edit(int id)
        {
            Book book = unitOfWork.Books.Get(id);
            if (book==null)
            {
                return HttpNotFound();
            }
            return View(book);
        }

        // POST: Books/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, Book book)
        {
            if (ModelState.IsValid)
            {
              unitOfWork.Books.Update(book);
              unitOfWork.Save();
              return RedirectToAction("Index");
            }
            return View(book);
        }

        // GET: Books/Delete/5
        public ActionResult Delete(int? id)
        {
            Book book=null;
            if (id!=null)
            book= unitOfWork.Books.Get((int)id);
            if (book == null)
            {
                return HttpNotFound();
              
            }
            return View(book);
        }

        // POST: Books/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Book book = unitOfWork.Books.Get(id);
            if (book != null)
            {
                unitOfWork.Books.Delete(id);
                unitOfWork.Save();
                return RedirectToAction("Index");
            }
            return View(book);
        }

        protected override void Dispose(bool disposing)
        {
            unitOfWork.Dispose();
            base.Dispose(disposing);
        }

    }
}
