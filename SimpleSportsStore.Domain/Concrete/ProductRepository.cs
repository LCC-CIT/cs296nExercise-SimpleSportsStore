using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SimpleSportsStore.Domain.Abstract;
using SimpleSportsStore.Domain.Entities;

namespace SimpleSportsStore.Domain.Concrete
{
   public class EFProductRepository : IProductRepository 
   {
        private EFDbContext context = new EFDbContext();

        public IQueryable<Product> Products 
        {
            get { return context.Products; } 
        }

        public void SaveProduct(Product product)   // Used for both Creating and Updating
        {

            if (product.ProductID == 0)   // Only add it if this is product isn't in the dB
            {
                context.Products.Add(product);
            }
            else   // If it's already in the dB, then update the property values
            {
                Product dbEntry = context.Products.Find(product.ProductID);  
                if (dbEntry != null)
                {
                    dbEntry.Name = product.Name;
                    dbEntry.Description = product.Description;
                    dbEntry.Price = product.Price;
                    dbEntry.Category = product.Category;
                }
            }
            context.SaveChanges();
        }

        public Product DeleteProduct(int productID)
        {
            Product dbEntry = context.Products.Find(productID);   // Make sure it's there first
            if (dbEntry != null)
            {
                context.Products.Remove(dbEntry);   // Delete it
                context.SaveChanges();
            }
            return dbEntry;
        }
    }
}