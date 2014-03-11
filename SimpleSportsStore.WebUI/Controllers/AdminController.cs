using SimpleSportsStore.Domain.Abstract;
using SimpleSportsStore.Domain.Concrete;
using SimpleSportsStore.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SimpleSportsStore.WebUI.Controllers
{
    public class AdminController : Controller
    {
        private IProductRepository repository;

        public AdminController()
        {
            repository = new EFProductRepository();
        }
        
        public AdminController(IProductRepository repo)
        {
            repository = repo;
        } 

        // GET: /Admin/
        // Returns all products
        public ViewResult Index()
        {
            return View(repository.Products);
        }

        public ViewResult Edit(int productId)
        {
            Product product = repository.Products
                .FirstOrDefault(p => p.ProductID == productId);
            return View(product);
        }

        [HttpPost]
        public ActionResult Edit(Product product)
        {
            if (ModelState.IsValid)
            {
                repository.SaveProduct(product);
                // TempData is a Dictionary that stores a key-value pair until the end of the HTTP request
                TempData["message"] = string.Format("{0} has been saved", product.Name);
                return RedirectToAction("Index");
            }
            else
            {
                // we got here because there is something wrong with the data values 
                return View(product);
            }
        }

        [HttpPost]
        public ActionResult Delete(int productId)
        {
            // Delete it!
            Product deletedProduct = repository.DeleteProduct(productId);
            if (deletedProduct != null)   // If it was deleted, it's not null
            {
                TempData["message"] = string.Format("{0} was deleted",
                    deletedProduct.Name);
            }
            return RedirectToAction("Index");  // Back to the main Admin page
        }

        public ViewResult Create()
        {
            return View("Edit", new Product()); // use a new Product object so the Edit view will render empty fields
        }

    }
}
