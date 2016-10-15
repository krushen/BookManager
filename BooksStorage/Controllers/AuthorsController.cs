using DAL;
using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
 

namespace BooksStorage.Controllers
{
    public class AuthorsController : Controller
    {
        UnitOfWork unitOfWork;
        public AuthorsController()
        {
            unitOfWork = new UnitOfWork();
        }
        // GET: Author
        public ActionResult Index()
        {
            var authors = unitOfWork.Authors.GetAll();
            return View(authors);
           
        }

        // GET: Author/Details/5
        public ActionResult Details(int id)
        {

            Author author = unitOfWork.Authors.Get(id);
            if (author == null)
            {
                return HttpNotFound();
            }
            return View(author);
        }

        // GET: Author/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Author/Create
        [HttpPost]
        public ActionResult Create(Author author)
        {
            if (ModelState.IsValid)
            {
                unitOfWork.Authors.Create(author);
                unitOfWork.Save();
                return RedirectToAction("Index");
            }

            return View(author);
        }

        // GET: Author/Edit/5
        public ActionResult Edit(int id)
        {
            Author author =  unitOfWork.Authors.Get(id);
            if (author == null)
            {
                return HttpNotFound();
            }
            return View(author);
            
        }

        // POST: Author/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, Author author)
        {
            if (ModelState.IsValid)
            {
                unitOfWork.Authors.Update(author);
                unitOfWork.Save();
                return RedirectToAction("Index");
            }
            return View(author);
        }

        // GET: Author/Delete/5
        public ActionResult Delete(int id)
        {
            //unitOfWork.Authors.Delete(id);
            //unitOfWork.Save();
            return RedirectToAction("Index");
        }

        // POST: Author/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            unitOfWork.Authors.Delete(id);
            unitOfWork.Save();
            return RedirectToAction("Index");
        }
        protected override void Dispose(bool disposing)
        {
            unitOfWork.Dispose();
            base.Dispose(disposing);
        }
    }
}
