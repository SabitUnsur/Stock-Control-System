using MvcStock.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcStock.Controllers
{
    public class SalesController : Controller
    {
        // GET: Sales

        MvcDbStokEntities db = new MvcDbStokEntities();
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult AddNewSale() 
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddNewSale(Tbl_Sales sale)
        {
            db.Tbl_Sales.Add(sale);
            db.SaveChanges();
            return View("Index");
        }
    }
}