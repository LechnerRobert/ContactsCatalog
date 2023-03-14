using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ContactsCatalog.Data;
using ContactsCatalog.Models;

namespace ContactsCatalogWebApp.Pages.Customers
{
    public class EditModel : PageModel
    {
        private readonly ContactsCatalog.Data.CustomerContext _context;

        public EditModel(ContactsCatalog.Data.CustomerContext context)
        {
            _context = context;
        }

        [BindProperty]
        public CustomerItemDTO CustomerItemDTO { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(long? id)
        {
            if (id == null )
            {
                return NotFound();
            }

            
            var customerItem = await _context.CustomerItems.FirstOrDefaultAsync(m => m.Id == id);
            if (customerItem == null)
            {
                return NotFound();
            }
            CustomerItemDTO = CustomerContext.CustomerToDTO(customerItem);
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync(long id)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            var customerItem = await _context.CustomerItems.FindAsync(id);
            if (customerItem == null)
            {
                return NotFound();
            }


            if (await TryUpdateModelAsync<CustomerItem>(
              customerItem,
              "CustomerItemDTO",
              s => s.Number, s => s.Name))

            { } else
            {
                return Page();
            }
            

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CustomerItemDTOExists(CustomerItemDTO.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool CustomerItemDTOExists(long id)
        {
          return (_context.CustomerItems?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
