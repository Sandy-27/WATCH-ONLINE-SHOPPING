using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WatchOnlineShopping.Models;
namespace WatchOnlineShopping.Controllers
{
    public class CategoryController : Controller
    {
        OnlineShoppingEntities1 onlineShoppingEntities = new OnlineShoppingEntities1();
        // GET: Category
        public ActionResult Index()
        {
            return View();
        }
        public PartialViewResult CategoryPartial()
        {
            var categoryList = onlineShoppingEntities.Categories.OrderBy(x => x.Name).ToList();
            return PartialView(categoryList);
        }
 
    }
}