using Microsoft.VisualStudio.TestTools.UnitTesting;
using CSharp.Controllers;
using System;
using System.Collections.Generic;
using System.Text;
using CSharp.Model;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace CSharp.Controllers.Tests
{
    [TestClass()]
    public class OrdersControllerTests
    {

        public static OrdersController ordersController;
        [ClassInitialize()]
        public static void OrdersControllerTestsInit(TestContext context)
        {
            ordersController = new OrdersController();
            _ = ordersController.CreateCustomer(new Customer { CustomerId = 1, CustomerName = "bob" });
            _ = ordersController.CreateCustomer(new Customer { CustomerId = 2, CustomerName = "James" });
            _ = ordersController.CreateCustomer(new Customer { CustomerId = 3, CustomerName = "Jake" });
            _ = ordersController.CreateOrder(new Order { CustomerId = 2, OrderCreatedDate = DateTime.Now });
        }

        [TestMethod()]
        public void CreateOrder_Test()
        {
            Order o = new Order();
            o.OrderCreatedDate = DateTime.Now;
            o.CustomerId = 1;

            int ordersCurrentCount = ordersController.Orders.Count;

            _ = ordersController.CreateOrder(o);

            Assert.IsTrue(ordersCurrentCount + 1 == ordersController.Orders.Count);
        }

        [TestMethod()]
        public async Task GetAllOrdersByCustomerIdTest()
        {
            //ordersController = new OrdersController();
            //ordersController.CreateCustomer(new Customer { CustomerId = 1, CustomerName = "bob" });
            //ordersController.CreateCustomer(new Customer { CustomerId = 2, CustomerName = "James" });
            //ordersController.CreateOrder(new Order { CustomerId = 2, OrderCreatedDate = DateTime.Now });
            var actionResult = await ordersController.GetAllOrdersByCustomerId(2);
            var result = actionResult.Result as OkObjectResult;
            var orders = (List<Order>)result.Value;
            Assert.IsTrue(orders.Count > 0);
        }

        [TestMethod()]
        public void UpdateOrderTest()
        {
            Order firstOrder = ordersController.Orders.FirstOrDefault();
            firstOrder.OrderId = 17;
            firstOrder.CustomerId = 3;
            firstOrder.OrderCreatedDate = DateTime.Now;
            _ = ordersController.UpdateOrder(firstOrder);

            Order updatedOrder = ordersController.Orders.FirstOrDefault(x => x.OrderId == 17);


            Assert.AreSame(firstOrder, updatedOrder);
        }

        [TestMethod()]
        public void CancelTest()
        {
            Order testOrder = ordersController.Orders.FirstOrDefault();
            testOrder.Status = OrderStatusEnum.ACTIVE;

            _ = ordersController.Cancel(testOrder);

            Assert.IsTrue(testOrder.Status == OrderStatusEnum.CANCELLED);
        }

        [TestMethod()]
        public void CancelTest1()
        {
            Order testOrder = ordersController.Orders.FirstOrDefault();
            testOrder.Status = OrderStatusEnum.ACTIVE;

            _ = ordersController.Cancel(testOrder.OrderId);

            Assert.IsTrue(testOrder.Status == OrderStatusEnum.CANCELLED);
        }

        [TestMethod()]
        public void CreateCustomerTest()
        {
            Customer c = new Customer { CustomerName = "Ashley" };

            int currentCountAshley = ordersController.Customers.Count(x => x.CustomerName == "Ashley");

            _ = ordersController.CreateCustomer(c);

            Assert.IsTrue(ordersController.Customers.Count(x => x.CustomerName == "Ashley") == currentCountAshley+1);


        }
    }
}