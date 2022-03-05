using CSharp.Model;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860


/*
 This is important! We want to respect your valuable time so PLEASE timebox this exercise.

Your current mission, should you choose to accept it, is to take a few (no more than two) hours to build an Orders REST API endpoint. We have created some starter templates for a few languages but you don't need to use them if you would prefer to roll your own.

What we would like to see from this exercise:
Create order endpoint
List all orders by customer endpoint
Update order endpoint
Cancel order endpoint
Tests to prove your code works as expected
Currently supported languages templates: C#, Go, Node.JS, Python

We are open to other languages if you feel more comfortable in them. We have expertises in Java, Kotlin, and a few others.

If you don't see your language of preference on the list of languages above, please reach out. We are happy to discuss other langages as well.

How to interact with this activity:
Fork this repo
Complete exercise with a language listed above (assuming you haven't chosen another, hipper language and discussed it with us)
Provide a link to the completed exercise to Jenny Hove [jenny.hove@sogeti.com]
Receive personalized code review/feedback session from our technical team
 */

namespace CSharp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private int orderId = 0;
        private List<Order> orders = new List<Order>();
        private List<Customer> customers = new List<Customer>();

        public List<Order> Orders { get => orders; set => orders = value; }
        public List<Customer> Customers { get => customers; set => customers = value; }

        // GET: api/<OrdersController>
        [HttpGet]
        public async Task<ActionResult> CreateOrder([FromBody] Order order)
        {
            order.OrderId = orderId++;
            order.Status = OrderStatusEnum.ACTIVE;
            Orders.Add(order);
            return Ok("Order Added: " + order.OrderId);
        }

        // GET api/<OrdersController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<List<Order>>> GetAllOrdersByCustomerId([FromBody] int customerId)
        {
            var p = Orders.Where(x => x.CustomerId == customerId).ToList();

            return Ok(Orders.Where(x => x.CustomerId == customerId).ToList());
        }

        // POST api/<OrdersController>
        [HttpPost]
        public async Task<ActionResult> UpdateOrder([FromBody] Order updated)
        {
            var index = Orders.IndexOf(Orders.FirstOrDefault(x => x.OrderId == updated.OrderId));
            if(index != -1)
            {
                Orders[index] = updated;
                return Ok("Updated");
            }
            return NotFound();
        }

        // PUT api/<OrdersController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult> Cancel([FromBody] Order order)
        {
            return await Cancel(order.OrderId);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Cancel([FromBody] int orderId)
        {
            var index = Orders.IndexOf(Orders.FirstOrDefault(x => x.OrderId == orderId));
            if (index != -1)
            {
                Orders[index].Status = OrderStatusEnum.CANCELLED;
                return Ok("Order Cancelled: " + orderId);
            }
            return NotFound();
        }

        // GET: api/<OrdersController>
        [HttpGet]
        public async Task<ActionResult> CreateCustomer([FromBody] Customer customer)
        {
            if (Customers.Any(x => x.CustomerId == customer.CustomerId))
            {
                customer.CustomerId = customers.OrderByDescending(x => x.CustomerId).FirstOrDefault().CustomerId + 1;
            }
            Customers.Add(customer);
            return Ok("Customer Added: " + customer.CustomerId);
        }
    }
}
