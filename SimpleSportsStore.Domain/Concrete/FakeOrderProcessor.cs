using SimpleSportsStore.Domain.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleSportsStore.Domain.Concrete
{
    public class OrderFlags
    {
        public bool OrderProcessed {get; set;}
    }

    public class FakeOrderProcessor : IOrderProcessor
    {
        private OrderFlags orderFlags;

        public FakeOrderProcessor(OrderFlags flags)
        {
            orderFlags = flags;
            orderFlags.OrderProcessed = false;
        }

        public void ProcessOrder(Entities.Cart cart, Entities.ShippingDetails shippingDetails)
        {
            orderFlags.OrderProcessed = true;
        }
    }
}
