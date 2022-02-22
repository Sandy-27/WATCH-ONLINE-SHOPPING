using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WatchOnlineShopping.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }
    }
}



//[Required]
//public string UserName { get; set; }
//[Required]
//[DataType(DataType.Password)]
//public string Password { get; set; }
//[Required]
//[Compare("Password", ErrorMessage = "Passwords do not match")]
//public string ConfirmPassword { get; set; }