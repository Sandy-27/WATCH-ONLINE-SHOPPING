//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Web;
//using System.Web.Mvc;
//using WatchOnlineShopping.Models;
//namespace WatchOnlineShopping.Controllers
//{
//    public class NewsController : Controller
//    {
//        OnlineShoppingEntities1 onlineShoppingEntities = new OnlineShoppingEntities1();
//        // GET: News
//        public ActionResult NewsIndex()
//        {
//            return View();
//        }
//        public PartialViewResult NewsListPartial()
//        {
//            var newsList = onlineShoppingEntities.News.OrderByDescending(x => x.NewsId).ToList();
//            return PartialView(newsList);
//        }
//    }
//}
//[Required]
//public string UserName { get; set; }
//[Required]
//[DataType(DataType.Password)]
//public string Password { get; set; }
//[Required]
//[Compare("Password", ErrorMessage = "Passwords do not match")]
//public string ConfirmPassword { get; set; }