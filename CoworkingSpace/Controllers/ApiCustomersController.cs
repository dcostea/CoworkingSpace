using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CoworkingSpace.Data;
using CoworkingSpace.Models;
using CoworkingSpace.Hubs;
using Microsoft.AspNetCore.SignalR;

namespace CoworkingSpace.Controllers
{
    [Route("api/customers")]
    [ApiController]
    public class ApiCustomersController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IHubContext<ChatHub> _hubContext;

        public ApiCustomersController(ApplicationDbContext context, IHubContext<ChatHub> hubContext)
        {
            _context = context;
            _hubContext = hubContext;
        }

        // GET: api/ApiCustomers
        [HttpGet]
        public IEnumerable<Customer> GetCustomers()
        {
            return _context.Customers;
        }

        [HttpGet]
        [Route("notify/{message}/group/{group}")]
        public async Task<IActionResult> NotifyGroup([FromRoute] string message, [FromRoute] string group)
        {
            var user = User.Claims.ToList()[1].Value;
            await _hubContext.Clients.Group(group).SendAsync("ReceiveGroupMessage", message, user);

            return Ok();
        }

        [HttpGet]
        [Route("notify/user/{message}")]
        public async Task<IActionResult> NotifyUser([FromRoute] string message)
        {
            var user = User.Claims.ToList()[1].Value;

            // Clients.User(user) has some bugs and the message is not received by frontend. User Clients.Group(user) instead
            //await _hubContext.Clients.User(user).SendAsync("ReceiveDirectMessage", message, user);
            await _hubContext.Clients.Group(user).SendAsync("ReceiveGroupMessage", message, user);

            return Ok();
        }

        [HttpGet]
        [Route("notify/all/{message}")]
        public async Task<IActionResult> NotifyCustomers([FromRoute] string message)
        {
            await _hubContext.Clients.All.SendAsync("ReceiveChatMessage", message, "no user");

            return Ok();
        }

        // GET: api/ApiCustomers/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCustomer([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var customer = await _context.Customers.FindAsync(id);

            if (customer == null)
            {
                return NotFound();
            }

            return Ok(customer);
        }

        // PUT: api/ApiCustomers/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCustomer([FromRoute] int id, [FromBody] Customer customer)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != customer.CustomerId)
            {
                return BadRequest();
            }

            _context.Entry(customer).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CustomerExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/ApiCustomers
        [HttpPost]
        public async Task<IActionResult> PostCustomer([FromBody] Customer customer)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Customers.Add(customer);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCustomer", new { id = customer.CustomerId }, customer);
        }

        // DELETE: api/ApiCustomers/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCustomer([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var customer = await _context.Customers.FindAsync(id);
            if (customer == null)
            {
                return NotFound();
            }

            _context.Customers.Remove(customer);
            await _context.SaveChangesAsync();

            return Ok(customer);
        }

        private bool CustomerExists(int id)
        {
            return _context.Customers.Any(e => e.CustomerId == id);
        }
    }
}