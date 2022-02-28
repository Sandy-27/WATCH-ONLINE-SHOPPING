using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WatchOnlineShopping.Models;
namespace WatchOnlineShopping.Controllers
{
    public class ProductsController : Controller
    {
        OnlineShoppingEntities1 onlineShoppingEntities = new OnlineShoppingEntities1();
        // GET: Products
        public ActionResult ProductsIndex()
        {
            return View();
        }
       
        public PartialViewResult productListPartial(int? category)
        {
           
            if (category != null)
            {
                //ViewBag.category = category;
                var productList = onlineShoppingEntities.Products.OrderByDescending(x => x.ProductId).Where(x => x.CategoryId == category).ToList();
                return PartialView(productList);
            }
        
            else
            {
                var productList = onlineShoppingEntities.Products.OrderByDescending(x => x.ProductId).ToList();
                return PartialView(productList);
            }
        }
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            }
            Product product = onlineShoppingEntities.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }



        public ActionResult AddImage()
        {
            Product product = new Product();
           
            OnlineShoppingEntities1 onlineShoppingEntities = new OnlineShoppingEntities1();
     
            return View(product);
        }

      

        [HttpPost]
        public ActionResult AddImage(Product model,HttpPostedFileBase image1)
        {
            OnlineShoppingEntities1 onlineShoppingEntities = new OnlineShoppingEntities1();
            if (image1 != null)
            {
                //convert img into binaryformat
                model.Image = new byte[image1.ContentLength];
                //inputstream- convert actual data to binary
                image1.InputStream.Read(model.Image,0,image1.ContentLength);//copy img to product class

            }
            onlineShoppingEntities.Products.Add(model);
            onlineShoppingEntities.SaveChanges();
            if (ModelState.IsValid)
            {
                ModelState.Clear();
            }
            return View(model);
        }
    }
}