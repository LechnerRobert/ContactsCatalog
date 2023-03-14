using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ContactsCatalog.Models;
using ContactsCatalog.Data;

namespace ContactsCatalog.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerItemsController : ControllerBase
    {
        private readonly CustomerContext _context;

        public CustomerItemsController(CustomerContext context)
        {
            _context = context;
        }

        // GET: api/CustomerItems
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CustomerItemDTO>>> GetCustomerItem()
        {
            return await _context.CustomerItems
                .Select(x => ItemToDTO(x))
                .ToListAsync();
        }

        // GET: api/CustomerItems/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CustomerItemDTO>> GetCustomerItem(long id)
        {
            var customerItem = await _context.CustomerItems.FindAsync(id);

            if (customerItem == null)
            {
                return NotFound();
            }

            return ItemToDTO(customerItem);
        }

        // PUT: api/CustomerItems/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCustomerItem(long id, CustomerItemDTO customerItemDTO)
        {
            if (id != customerItemDTO.Id)
            {
                return BadRequest();
            }

            _context.Entry(customerItemDTO).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CustomerItemExists(id))
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

        // POST: api/CustomerItems
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<CustomerItemDTO>> PostCustomerItem(CustomerItemDTO customerItemDTO)
        {
            var customerItem = new CustomerItem
            {
                Id = customerItemDTO.Id,
                Name = customerItemDTO.Name,
                Number = customerItemDTO.Number

            };
            
            _context.CustomerItems.Add(customerItem);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetCustomerItem), new { id = customerItem.Id }, customerItem);
        }

        // DELETE: api/CustomerItems/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCustomerItem(long id)
        {
            var customerItem = await _context.CustomerItems.FindAsync(id);
            if (customerItem == null)
            {
                return NotFound();
            }

            _context.CustomerItems.Remove(customerItem);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CustomerItemExists(long id)
        {
            return _context.CustomerItems.Any(e => e.Id == id);
        }
        private static CustomerItemDTO ItemToDTO(CustomerItem todoItem) =>
          new CustomerItemDTO
          {
              Id = todoItem.Id,
              Name = todoItem.Name,
              Number = todoItem.Number
          };
    }

}
