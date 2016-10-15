using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestEFDB
{
    class Program
    {
        static void Main(string[] args)
        {
            UnitOfWork unitOfWork = new UnitOfWork();
            var authors = unitOfWork.Authors.GetAll();
            foreach (var item in authors)
            {
                Console.WriteLine(item.FirstName+" "+item.LastName);
            }
            unitOfWork.Dispose();
        }
    }
}
