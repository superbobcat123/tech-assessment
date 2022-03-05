using System;
using System.Collections.Generic;

namespace CSharp.Model
{
    public class Order
    {
        private int orderId;
        private int customerId;
        private DateTime orderCreatedDate;
        //private string orderName;
        //private List<Product> products;
        private OrderStatusEnum status;

        public int OrderId { get => orderId; set => orderId = value; }
        public int CustomerId { get => customerId; set => customerId = value; }
        public DateTime OrderCreatedDate { get => orderCreatedDate; set => orderCreatedDate = value; }
        //public string OrderName { get => orderName; set => orderName = value; }
        //public List<Product> Products { get => products; set => products = value; }
        public OrderStatusEnum Status { get => status; set => status = value; }
    }

}
