using InventoryApplication.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace InventoryApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly InventoryDbContext _context;

        public CustomerController(InventoryDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult> GetAll()
        {
            var customers = await _context.CustomerTbls.ToListAsync();
            return Ok(customers);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetById(int id)
        {
            var customer = await _context.CustomerTbls.FirstOrDefaultAsync(x => x.CustomerId == id);
            if (customer == null)
            {
                return NotFound();

            }
            return Ok(customer);

        }
        [HttpPost]
        public async Task<ActionResult> AddCustomer(CustomerTbl customer)
        {
            _context.CustomerTbls.Add(customer);
            await _context.SaveChangesAsync();
            return Ok(customer);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateCustomer(int id, CustomerTbl customer)
        {
            var existingCustomer = await _context.
                CustomerTbls.FirstOrDefaultAsync(x => x.CustomerId == id);
            if (existingCustomer == null)
            {
                return NotFound();

            }
            existingCustomer.CustomerEmail = customer.CustomerEmail;
            existingCustomer.CustomerPhone = customer.CustomerPhone;
            existingCustomer.RegistrationDate = customer.RegistrationDate;

            return Ok(existingCustomer);





        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteCustomer(int id)
        {
            var customer = await _context.
                CustomerTbls.FirstOrDefaultAsync(x => x.CustomerId == id);
            if (customer == null)
            {
                return NotFound();
            }

            _context.CustomerTbls.Remove(customer);
            await _context.SaveChangesAsync();
            return Ok("Customer Deleted Successfully");
        }
    }
}
