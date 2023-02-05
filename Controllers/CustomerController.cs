using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcStock.Models.Entity;

namespace MvcStock.Controllers
{
    public class CustomerController : Controller
    {
        // GET: Customer

        MvcDbStokEntities db=new MvcDbStokEntities();
        public ActionResult Index(string param)
        {
            var values = db.Tbl_Customers.ToList();

            if (!string.IsNullOrEmpty(param))
            {
                values=values.Where(p=>p.CustomerName.Contains(param)).ToList();
            }
           
            return View(values);
        }

        [HttpGet]
        public ActionResult AddNewCustomer()
        { 
            return View(); 
        }

        [HttpPost]
        public ActionResult AddNewCustomer(Tbl_Customers customer) 
        { 
            if(!ModelState.IsValid)
            {
                return View("AddNewCustomer");
            }
            db.Tbl_Customers.Add(customer);
            db.SaveChanges();
            return RedirectToAction("Index");

        }

        public ActionResult DELETE(int id) 
        {
            var customer = db.Tbl_Customers.Find(id);
            db.Tbl_Customers.Remove(customer);
            db.SaveChanges();
            return RedirectToAction("Index");
        
        }

        [HttpGet]
        public ActionResult GetCustomer(int id) 
        {
            var customer = db.Tbl_Customers.Find(id);
            return View("GetCustomer",customer);
        }

        [HttpPost]
        public ActionResult Update(Tbl_Customers tbl_Customers) 
        {
            var Updatedcustomer = db.Tbl_Customers.Find(tbl_Customers.CustomerID);
            Updatedcustomer.CustomerName = tbl_Customers.CustomerName;
            Updatedcustomer.CustomerSurname=tbl_Customers.CustomerSurname;
            db.SaveChanges();
            return RedirectToAction("Index");
          
        }


    }
}