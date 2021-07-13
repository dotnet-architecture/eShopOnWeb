using System;
using System.Collections.Generic;
using System.Text;

namespace DeliveryOrderProcessor
{
    public class Order
    {
        public string OrderId { get; set;}

        public string ShippingAddress { get; set; }
            
        public OrderItem[] Items { get; set; }
           
        public double Total { get; set; }
    }

    public class OrderItem
    {
        public string Id { get; set; }

        public int Units { get; set; }
    }
}
