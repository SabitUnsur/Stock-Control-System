using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcStock.Models.Entity;
using PagedList;
using PagedList.Mvc;

namespace MvcStock.Controllers
{
    public class CategoryController : Controller
    {
        // GET: Category

        MvcDbStokEntities db= new MvcDbStokEntities();

        public ActionResult Index(int page=1)
        {
            //var values=db.Tbl_Categories.ToList();
            var values = db.Tbl_Categories.ToList().ToPagedList(page, 4);
            return View(values);
        }

        [HttpGet] 
        public ActionResult AddNewCategory() 
        { 
            return View(); 
        } //without any action

        [HttpPost]
        public ActionResult AddNewCategory(Tbl_Categories category)
        {
            if (!ModelState.IsValid)
            {
                //ModelState.AddModelError(nameof(Tbl_Categories.CategoryName), "This field is required!");
                return View("AddNewCategory");
            }
            db.Tbl_Categories.Add(category);
            db.SaveChanges();
            return RedirectToAction("Index"); 
        }
        public ActionResult DELETE(int id) 
        {
            var category = db.Tbl_Categories.Find(id);
            db.Tbl_Categories.Remove(category);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult GetCategory(int id) 
        {
            var category = db.Tbl_Categories.Find(id);
            return View("GetCategory",category);
        }

        [HttpPost]
        public ActionResult Update(Tbl_Categories categories) 
        {
            var category = db.Tbl_Categories.Find(categories.CategoryID);
            category.CategoryName = categories.CategoryName;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

    }
}