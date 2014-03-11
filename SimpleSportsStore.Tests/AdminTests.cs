using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Linq;
using SimpleSportsStore.Domain.Concrete;
using SimpleSportsStore.Domain.Entities;
using SimpleSportsStore.WebUI.Controllers;

namespace SimpleSportsStore.WebUI.Tests
{
    [TestClass]
    public class AdminTests
    {
        [TestMethod]
        public void Index_Contains_All_Products()
        {
            // Arrange - create the mock repository with three products
            var mockRepo = new FakeProductRepository(new List<Product> {
                            new Product {ProductID = 1, Name = "P1"},
                            new Product {ProductID = 2, Name = "P2"},
                            new Product {ProductID = 3, Name = "P3"} });

            // Arrange - create an Admin controller  
            AdminController target = new AdminController(mockRepo);

            // Action - Qyery for all products
            // Note: We're getting an array of Products out of the 
            // ViewData returned by the AdminController.Index method
            Product[] result = ((IEnumerable<Product>)target.Index().
                ViewData.Model).ToArray();

            // Assert
            Assert.AreEqual(result.Length, 3);
            Assert.AreEqual("P1", result[0].Name);
            Assert.AreEqual("P2", result[1].Name);
            Assert.AreEqual("P3", result[2].Name);
        }

        [TestMethod]
        public void Can_Edit_Product()
        {

            // Arrange - create the mock repository with three products
            var mockRepo = new FakeProductRepository(new List<Product> {
                            new Product {ProductID = 1, Name = "P1"},
                            new Product {ProductID = 2, Name = "P2"},
                            new Product {ProductID = 3, Name = "P3"} });



            // Arrange - create the controller
            AdminController target = new AdminController(mockRepo);

            // Act
            Product p1 = target.Edit(1).ViewData.Model as Product;
            Product p2 = target.Edit(2).ViewData.Model as Product;
            Product p3 = target.Edit(3).ViewData.Model as Product;

            // Assert
            Assert.AreEqual(1, p1.ProductID);
            Assert.AreEqual(2, p2.ProductID);
            Assert.AreEqual(3, p3.ProductID);
        }

        [TestMethod]
        public void Cannot_Edit_Nonexistent_Product()
        {
            // Arrange - create the mock repository with three products
            var mockRepo = new FakeProductRepository(new List<Product> {
                            new Product {ProductID = 1, Name = "P1"},
                            new Product {ProductID = 2, Name = "P2"},
                            new Product {ProductID = 3, Name = "P3"} });



            // Arrange - create the controller
            AdminController target = new AdminController(mockRepo);

            // Act
            Product result = (Product)target.Edit(4).ViewData.Model;

            // Assert
            Assert.IsNull(result);
        }

        [TestMethod]
        public void Can_Save_Valid_Changes()
        {

            // Arrange - create mock repository
            var mockRepo = new FakeProductRepository();
            // Arrange - create the controller
            AdminController target = new AdminController(mockRepo);
            // Arrange - create a product
            Product product = new Product { Name = "Test", ProductID = 0 }; // remember, we're not testing the repository

            // Act - try to save the product
            ActionResult result = target.Edit(product);

            // Assert - check that the repository has a product
            Assert.IsTrue(mockRepo.Products.Count() == 1);
            // Assert - check the method result type
            Assert.IsNotInstanceOfType(result, typeof(ViewResult));
        }

        [TestMethod]
        public void Cannot_Save_Invalid_Changes()
        {

            // Arrange - create mock repository 
            var mockRepo = new FakeProductRepository();
            // Arrange - create the controller
            AdminController target = new AdminController(mockRepo);
            // Arrange - create a product
            Product product = new Product { Name = "Test", ProductID = 0 };
            // Arrange - add an error to the model state
            target.ModelState.AddModelError("error", "error");

            // Act - try to save the product
            ActionResult result = target.Edit(product);

            // Assert - check that the repository does not have a product
            Assert.IsTrue(mockRepo.Products.Count() == 0);
            // Assert - check the method result type
            Assert.IsInstanceOfType(result, typeof(ViewResult));
        }

        [TestMethod]
        public void Can_Delete_Valid_Products()
        {

            // Arrange - create a Product
            Product prod = new Product { ProductID = 2, Name = "Test" };

            // Arrange - create the mock repository and add three products, including prod
            var mockRepo = new FakeProductRepository(new List<Product> { new Product {ProductID = 1, Name = "P1"},
                                prod, new Product {ProductID = 3, Name = "P3"} } );
            // Arrange - create the controller
            AdminController target = new AdminController(mockRepo);

            // Act - delete the product
            target.Delete(prod.ProductID);

            // Assert - ensure that the repository delete method deleted the correct Product 
            Assert.IsNull(mockRepo.Products.FirstOrDefault(p => p.ProductID == prod.ProductID));
        }
    }
}
