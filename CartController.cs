using ShoppingCartApp.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace ShoppingCartApp.Controllers
{
    public class CartController : Controller
    {
        public ActionResult AddToCart(int id)
        {
            List<Product> products = new List<Product>()
            {
                new Product { ProductID = 1, ProductName = "Laptop", Price = 55000 },
                new Product { ProductID = 2, ProductName = "Mouse", Price = 600 },
                new Product { ProductID = 3, ProductName = "Keyboard", Price = 1500 },
                new Product { ProductID = 4, ProductName = "Monitor", Price = 12000 }
            };

            Product product = products.FirstOrDefault(x => x.ProductID == id);

            List<CartItem> cart;

            if (Session["Cart"] == null)
            {
                cart = new List<CartItem>();
            }
            else
            {
                cart = (List<CartItem>)Session["Cart"];
            }

            CartItem item = cart.FirstOrDefault(
                x => x.Product.ProductID == id);

            if (item == null)
            {
                cart.Add(new CartItem
                {
                    Product = product,
                    Quantity = 1
                });
            }
            else
            {
                item.Quantity++;
            }

            Session["Cart"] = cart;

            return RedirectToAction("Index", "Home");
        }

        public ActionResult Index()
        {
            List<CartItem> cart;

            if (Session["Cart"] == null)
            {
                cart = new List<CartItem>();
            }
            else
            {
                cart = (List<CartItem>)Session["Cart"];
            }

            return View(cart);
        }

        public ActionResult Remove(int id)
        {
            List<CartItem> cart = Session["Cart"] as List<CartItem>;

            if (cart != null)
            {
                CartItem item = cart.FirstOrDefault(
                    x => x.Product.ProductID == id);

                if (item != null)
                {
                    cart.Remove(item);
                }

                Session["Cart"] = cart;
            }

            return RedirectToAction("Index");
        }
    }
}
