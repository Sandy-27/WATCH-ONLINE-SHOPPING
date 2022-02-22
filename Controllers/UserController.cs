using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using WatchOnlineShopping.Models;

namespace WatchOnlineShopping.Controllers
{
    public class UserController : Controller
    {
        OnlineShoppingEntities1 onlineShoppingEntities = new OnlineShoppingEntities1();
        private string strCart = "Cart";
        // GET: Register
        [AllowAnonymous]
        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        [AllowAnonymous]
        public ActionResult Register(UsersLogin user)
        {
            OnlineShoppingEntities1 onlineShoppingEntities = new OnlineShoppingEntities1();
            onlineShoppingEntities.UsersLogins.Add(user);
            onlineShoppingEntities.SaveChanges();
            string message = string.Empty;
            switch (user.UserId)
            {
                case -1:
                    message = "Username already exists.\\nPlease choose a different user name.";
                    break;

                default:
                    message = "Registration successful.";
                    FormsAuthentication.SetAuthCookie(user.UserName, false);
                    return RedirectToAction("Login");
                   
            }
            ViewBag.Message = message;

            return View(user);
        }
        [AllowAnonymous]
        public ActionResult Login() {
            return View();
        }
        [HttpPost]
        [AllowAnonymous]
        public ActionResult Login(UsersLogin user )
        {
            OnlineShoppingEntities1 onlineShoppingEntities = new OnlineShoppingEntities1();
            int? userId = onlineShoppingEntities.Validate_User(user.UserName, user.Password).FirstOrDefault();

            string message = string.Empty;
            switch (userId.Value)
            {
                case -1:
                        message = "Your password is incorrect.\\nGive the correct Password to login";                  
                    break;
                case -2:
                    message = "Username does not exists.\\nRegister before you login";
                    break;
                default:
                    
                    FormsAuthentication.SetAuthCookie(user.UserName,false);
                    return RedirectToAction("CheckOut");
            }

            ViewBag.Message = message;
            return View(user);
        }
        public ActionResult CheckOut(FormCollection frc)
        {

            return View("CheckOut");
        }
        public ActionResult ProcessOrder(FormCollection frc)
        {
            List<Cart> iscart = (List<Cart>)Session[strCart];
            //save the order into order table
            Order order = new Order()
            {
                CustomerName = frc["cusName"],
                CustomerPhone = frc["cusPhone"],
                CustomerEmail = frc["cusEmail"],
                CustomerAddress = frc["cusAddress"],
                OrderDate = DateTime.Now,
                PaymentType = "Cash",
                Status = "Processing"

            };
            onlineShoppingEntities.Orders.Add(order);
            onlineShoppingEntities.SaveChanges();
            //save the order detail into orderdetail table
            foreach (Cart cart in iscart)
            {
                OrderDetail orderDetail = new OrderDetail()
                {
                    OrderId = order.OrderId,
                    ProductId = cart.Product.ProductId,
                    Quantity = cart.Quantity,
                    Price = cart.Product.Price.ToString()
                };
                onlineShoppingEntities.OrderDetails.Add(orderDetail);
                onlineShoppingEntities.SaveChanges();
            }
            //remove shopping cart
            Session.Remove(strCart);
            return View("OrderSuccess");
        }
    }
}
