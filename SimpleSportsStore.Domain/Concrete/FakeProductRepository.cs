using SimpleSportsStore.Domain.Abstract;
using SimpleSportsStore.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleSportsStore.Domain.Concrete
{
    public class FakeProductRepository : IProductRepository
    {

        private List<Product> products;

        public FakeProductRepository()
        {
            products = new List<Product>();
            /* We don't want any default products any more */
            /* products = new List<Product> {
                new Product { Name = "Football", Price = 25 },
                new Product { Name = "Surf board", Price = 179 },
                new Product { Name = "Running shoes", Price = 95 }
                };
             */
        }

        public FakeProductRepository(List<Product> p)
        {
            products = p;
        }

        public IQueryable<Product> Products
        {
            get { return products.AsQueryable(); }
        }


        void IProductRepository.SaveProduct(Product product)
        {
            if (product.ProductID == 0)
            {
                products.Add(product);
            }
            else   // I probably didn't need to add this logic - we're not going to test this fake repo!
            {
                Product dbEntry = products.Find(p => p.ProductID == product.ProductID);
                if (dbEntry != null)
                {
                    dbEntry.Name = product.Name;
                    dbEntry.Description = product.Description;
                    dbEntry.Price = product.Price;
                    dbEntry.Category = product.Category;
                }
            }
        }


        public Product DeleteProduct(int productID)
        {
            Product prodToDelete = null;
            foreach (Product p in products)
            {
                if (p.ProductID == productID)
                {
                    prodToDelete = p;
                    products.Remove(p);
                    break;
                }
            }
            return prodToDelete;
        }
    }
}
