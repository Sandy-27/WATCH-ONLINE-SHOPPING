using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WatchOnlineShopping.Models;

namespace WatchOnlineShopping.Controllers
{
    public class ShoppingCartController : Controller
    {
        OnlineShoppingEntities1 onlineShoppingEntities = new OnlineShoppingEntities1();
        private string strCart = "Cart";
        // GET: ShoppingCart
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult OrderNow(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            }
            if (Session[strCart] == null)
            {
                List<Cart> iscart = new List<Cart>
                {
                    new Cart(onlineShoppingEntities.Products.Find(id),1)
                };
                Session[strCart] = iscart;
            }
            else
            {
                List<Cart> iscart = (List<Cart>)Session[strCart];
                int isCheck= isExistingCheck(id);
                if (isCheck == -1)
                    iscart.Add(new Cart(onlineShoppingEntities.Products.Find(id), 1));
                else
                    iscart[isCheck].Quantity++;
             
                Session[strCart] = iscart;

            }
            return RedirectToAction("Index");
        }
        private int isExistingCheck(int? id)
        {
            List<Cart> iscart = (List<Cart>)Session[strCart];
            for(int check=0;check<iscart.Count;check++)
            {
                if (iscart[check].Product.ProductId == id)
                    return check;
            }
            return -1;
        }
        public ActionResult Delete(int? id)
        {          
            if (id == null)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            }
            int isCheck = isExistingCheck(id);
            if (isCheck >= 0)
            {
                List<Cart> iscart = (List<Cart>)Session[strCart];

                iscart.RemoveAt(isCheck); }          
            else
            {
                isExistingCheck(id);
            }
            return View("Index");
        }
        public ActionResult UpdateCart(FormCollection frc)
        {
            string[] quantities = frc.GetValues("quantity");
            List<Cart> isCart = (List<Cart>)Session[strCart];
            for (int i = 0; i < isCart.Count; i++)
            {
                isCart[i].Quantity = Convert.ToInt32(quantities[i]);
            }
            Session[strCart] = isCart;
            return View("Index");
        }
       
       
    }
}
