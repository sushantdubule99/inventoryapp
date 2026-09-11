using InventoryApplication.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace InventoryApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BillController : ControllerBase
    {
        public readonly InventoryDbContext _context;

        public BillController(InventoryDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult> GetAll()
        {
            var bills = await _context.BillTbls.ToListAsync();
            return Ok(bills);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetById(int id)
        {
            {
                var bill = await _context.BillTbls.FirstOrDefaultAsync(x => x.BillId == id);
                if (bill == null)
                {
                    return NotFound();
                }
                return Ok(bill);

            }
        }


        [HttpPost]
        public async Task<ActionResult> AddBill(BillTbl bill)
        {
            _context.BillTbls.Add(bill);
            await _context.SaveChangesAsync();
            return Ok(bill);
        }


        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateBill(int id, BillTbl bill)
        {
            var existingBill = await _context.BillTbls.FirstOrDefaultAsync(x => x.BillId == id);
            if (existingBill == null)
            {
                return NotFound();
            }
            existingBill.CustomerId = bill.CustomerId;
            existingBill.ProductId = bill.ProductId;
            existingBill.Quantity = bill.Quantity;
            existingBill.Price = bill.Price;
            existingBill.Amount = bill.Amount;
            existingBill.BillDate = bill.BillDate;
            await _context.SaveChangesAsync();
            return Ok(existingBill);
        }




        [HttpDelete("{id}")]

        public async Task<ActionResult> DeleteBill(int id)
        {
            var bill = await _context.BillTbls.FirstOrDefaultAsync(x => x.BillId == id);
            if (bill == null)
            {
                return NotFound();
            }
            _context.BillTbls.Remove(bill);
            await _context.SaveChangesAsync();
            return Ok(bill);
        }

        [HttpGet("customer/{customerId}")]
        public async Task<ActionResult> GetBillById(int customerId)
        {
            var bills = await _context.BillTbls.Where(x => x.CustomerId == customerId).ToListAsync();
            if (bills == null || bills.Count == 0)
            {
                return NotFound();
            }
            return Ok(bills);
        }

        [HttpGet("product/{productId}")] 

        public async Task<ActionResult> GetBillByProductId(int productId)
        {
            var bills = await _context.BillTbls.Where(x => x.ProductId == productId).ToListAsync();
            if (bills == null || bills.Count == 0)
            {
                return NotFound();
            }
            return Ok(bills);
        }

    }
}
