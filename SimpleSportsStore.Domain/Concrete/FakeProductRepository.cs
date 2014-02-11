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
            products = new List<Product> {
                new Product { Name = "Football", Price = 25 },
                new Product { Name = "Surf board", Price = 179 },
                new Product { Name = "Running shoes", Price = 95 }
                };
        }

        public FakeProductRepository(List<Product> p)
        {
            products = p;
        }

        public IQueryable<Product> Products
        {
            get { return products.AsQueryable(); }
        }

    }
}
