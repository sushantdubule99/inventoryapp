using InventoryApplication.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace InventoryApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InventoryController : ControllerBase
    {
        private readonly InventoryDbContext _context;

        public InventoryController(InventoryDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult> GetAll()
        {
            var inventory = await _context.Inventories.ToListAsync();
            return Ok(inventory);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetById(int id)
        {
            var inventory = await _context.Inventories.FirstOrDefaultAsync(x => x.ProductId == id);
            if (inventory == null)
            {
                return NotFound("Product not Found");
            }
            return Ok(inventory);

        }

        [HttpPost]
        public async Task<ActionResult> Create(Inventory inventory)
        {
            _context.Inventories.Add(inventory);
            await _context.SaveChangesAsync();
            return Ok(inventory);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Inventory inventory)
        {
            var existingInventory = await _context.Inventories.FirstOrDefaultAsync(x => x.ProductId == id);
            if (existingInventory == null)
            {
                return NotFound("Product not found");
            }
            existingInventory.ProductName = inventory.ProductName;
            existingInventory.StockAvailable = inventory.StockAvailable;
            existingInventory.RecordStock = inventory.RecordStock;
            await _context.SaveChangesAsync();
            return Ok(existingInventory);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var inventory = await _context.Inventories.FirstOrDefaultAsync(x => x.ProductId == id);
            if (inventory == null)
            {
                return NotFound("Product not found");
            }

            _context.Inventories.Remove(inventory);
            await _context.SaveChangesAsync();
            return Ok("Product Deleted Successfully");

        }
    }
}
