using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcStock.Models.Entity;
namespace MvcStock.Controllers
{
    public class ProductController : Controller
    {
        // GET: Product

        MvcDbStokEntities db=new MvcDbStokEntities();

        public ActionResult Index()
        {
            var values=db.Tbl_Products.ToList();
            return View(values);
        }

        [HttpGet]
        public ActionResult AddNewProduct()
        {
            List<SelectListItem> values = (from p in db.Tbl_Categories.ToList()
                                           select new SelectListItem
                                           {
                                               Text = p.CategoryName,
                                               Value= p.CategoryID.ToString(),
                                          }).ToList(); 
            ViewBag.value = values;
            //To move the expression on Controller to the other page

            return View("AddNewProduct");
        }

        [HttpPost]
        public ActionResult AddNewProduct(Tbl_Products product)
        {
            var category = db.Tbl_Categories.Where(c => c.CategoryID == product.Tbl_Categories.CategoryID).FirstOrDefault();
            //Bizim seçtiğimiz kategori ID sine eşit olan kategori ID sini bulup category değişkenine atıyor. 
            
            product.Tbl_Categories =category; //Ürünün hangi kategoriye ait olduğunun eşleşmesi yapılır.

            db.Tbl_Products.Add(product);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult DELETE(int id) 
        {
            var product = db.Tbl_Products.Find(id);
            db.Tbl_Products.Remove(product); db.SaveChanges();
            return RedirectToAction("Index");

        }

        [HttpGet]
        public ActionResult GetProduct(int id)
        {
            var products = db.Tbl_Products.Find(id);
            List<SelectListItem> values = (from p in db.Tbl_Categories.ToList()
                                           select new SelectListItem
                                           {
                                               Text = p.CategoryName,
                                               Value = p.CategoryID.ToString(),
                                           }).ToList();
            ViewBag.value = values;
            return View("GetProduct",products);
        }

        [HttpPost]
        public ActionResult Update(Tbl_Products products)
        {
            var Updatedproduct = db.Tbl_Products.Find(products.productID);
            Updatedproduct.productName = products.productName;
            Updatedproduct.productPrice = products.productPrice;

            //var Updatedcategory = db.Tbl_Categories.Where(c => c.CategoryID == products.Tbl_Categories.CategoryID).FirstOrDefault();
            //Updatedproduct.Tbl_Categories = Updatedcategory;

            Updatedproduct.productCategory = products.productCategory;

            Updatedproduct.productBrand = products.productBrand;
            Updatedproduct.productStock = products.productStock;
            db.SaveChanges();
            return RedirectToAction("Index");
        }




    }
}