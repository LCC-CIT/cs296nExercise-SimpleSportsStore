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

        public IQueryable<Product> Products {
            get { return context.Products; } 
        } 
    }
}