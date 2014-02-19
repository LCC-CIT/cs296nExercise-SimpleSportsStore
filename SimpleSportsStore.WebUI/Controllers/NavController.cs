using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SimpleSportsStore.Domain.Abstract;
using SimpleSportsStore.Domain.Concrete;

namespace SimpleSportsStore.WebUI.Controllers
{
    public class NavController : Controller
    {
        /* // just for testing
        public string Menu()
        {
            return "Hello from NavController";
        }
        */

        private IProductRepository repository;

        // Since we're not using Ninject, we just create a repository
        // in this default constructor
        public NavController() 
        {
            repository = new EFProductRepository();
        }

        // This overloaded constructor is just for testing
         public NavController(IProductRepository productRepository)
        {
            repository = productRepository;
        }

        public PartialViewResult Menu(string category = null) 
        {
            ViewBag.SelectedCategory = category;

            IEnumerable<string> categories = repository.Products
                                                .Select(x => x.Category)
                                                .Distinct()
                                                .OrderBy(x => x);
            return PartialView(categories);
        } 
    }
}
