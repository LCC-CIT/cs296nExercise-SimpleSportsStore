﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SimpleSportsStore.Domain.Abstract;
using SimpleSportsStore.Domain.Concrete;
using SimpleSportsStore.WebUI.Models;

namespace SimpleSportsStore.WebUI.Controllers
{
    public class ProductController : Controller
    {
        private IProductRepository repository;
        public int PageSize = 4;       // 4 products per page

        public ProductController()
        {
            // repository = new FakeProductRepository();
            repository = new EFProductRepository();
        }

        public ProductController(IProductRepository productRepository)
        {
            repository = productRepository;
        }

        public ViewResult List(string category, int page = 1)
        {
            /*
            return View(repository.Products
                .OrderBy(p => p.ProductID)
                .Skip((page - 1) * PageSize)
                .Take(PageSize)
                );
             */
            ProductsListViewModel model = new ProductsListViewModel {
                // Get a list of products, put it in the model
                Products = repository.Products
                    .Where(p => category == null || p.Category == category) // filter by category
                    .OrderBy(p => p.ProductID)
                    .Skip((page - 1) * PageSize)
                    .Take(PageSize),
                PagingInfo = new PagingInfo {
                    CurrentPage = page,
                    ItemsPerPage = PageSize,
                    TotalItems =  category == null ? repository.Products.Count() :
                                                repository.Products.Where(e => e.Category == category).Count() },
                CurrentCategory = category
            };

            return View(model);
        }

    }
}
