using DAL;
using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BookStorage.Models;
using Google.DataTable.Net.Wrapper;
using System.IO;

namespace BookStorage.Controllers
{
   
    public class ChartController : ApiController
    {
        UnitOfWork unitOfWork;
      
        // GET: Books
        public ChartController()
        {
            unitOfWork = new UnitOfWork();
            }
        [HttpPost]
        [Route("api/charts/books-chart/{id}")]
       
        public HttpResponseMessage BookChart(int id)
        {
            Book book = unitOfWork.Books.Get((int)id);
            if (book == null) return Request.CreateResponse(HttpStatusCode.BadRequest, new ApiError("Book doesn't exit"));
            IEnumerable<Keeper> AvgKeepers = unitOfWork.Keepers.GetAvgKeepers((id));
            return Request.CreateResponse(HttpStatusCode.OK, AvgKeepers);
        }

       
        protected override void Dispose(bool disposing)
        {
            unitOfWork.Dispose();
            base.Dispose(disposing);
        }
    }


}
