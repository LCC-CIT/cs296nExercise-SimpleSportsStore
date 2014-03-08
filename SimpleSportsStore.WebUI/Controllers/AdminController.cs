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
                TempData["message"] = string.Format("{0} has been saved", product.Name);
                // TempData is a Dictionary that stores a key-value pair until the end of the HTTP request
                return RedirectToAction("Index");
            }
            else
            {
                // there is something wrong with the data values 
                return View(product);
            }
        }


    }
}
