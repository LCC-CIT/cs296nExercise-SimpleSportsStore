using SimpleSportsStore.Domain.Abstract;
using SimpleSportsStore.Domain.Concrete;
using SimpleSportsStore.Domain.Entities;
using SimpleSportsStore.WebUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SimpleSportsStore.WebUI.Controllers
{
    public class CartController : Controller
    {
        private IProductRepository repository;

        public CartController()
        {
            repository = new EFProductRepository();
        }

        public CartController(IProductRepository repo)
        {
            repository = repo;
        }

        public ViewResult Index(Cart cart, string returnUrl)
        {
            return View(new CartIndexViewModel {
                Cart = cart,
                ReturnUrl = returnUrl}
                );
        }

        // Receives POST variables from ProductSummary view
        public RedirectToRouteResult AddToCart(Cart cart, int productId, string returnUrl)
        {
            Product product = repository.Products
                .FirstOrDefault(p => p.ProductID == productId);
            if (product != null)
            {
                cart.AddItem(product, 1);
            }
            // Send a HTTP redirect instruction to the browser
            return RedirectToAction("Index", new { returnUrl });
        }

        // Receives POST variables from ProductSummary view
        public RedirectToRouteResult RemoveFromCart(Cart cart, int productId, string returnUrl)
        {
            Product product = repository.Products
                .FirstOrDefault(p => p.ProductID == productId);
            if (product != null)
            {
                cart.RemoveLine(product);
            }
            // Send a HTTP redirect instruction to the browser 
            return RedirectToAction("Index", new { returnUrl });    
        }

        /* This is done by the CartModelBinder now
        // This method allows the cart object to persist between http requests
        private Cart GetCart()
        {
            Cart cart = (Cart)Session["Cart"];
            if (cart == null)
            {
                cart = new Cart();
                Session["Cart"] = cart;
            }
            return cart;
        }
        */
    }
}
