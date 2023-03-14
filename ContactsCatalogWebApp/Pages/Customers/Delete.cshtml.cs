using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ContactsCatalog.Data;
using ContactsCatalog.Models;

namespace ContactsCatalogWebApp.Pages.Customers
{
    public class DeleteModel : PageModel
    {
        private readonly ContactsCatalog.Data.CustomerContext _context;

        public DeleteModel(ContactsCatalog.Data.CustomerContext context)
        {
            _context = context;
        }

        [BindProperty]
      public CustomerItem CustomerItemDTO { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(long? id)
        {
            if (id == null || _context.CustomerItems == null)
            {
                return NotFound();
            }

            var customeritem = await _context.CustomerItems.FirstOrDefaultAsync(m => m.Id == id);

            if (customeritem == null)
            {
                return NotFound();
            }
            else 
            {
                CustomerItemDTO = customeritem;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(long? id)
        {
            if (id == null || _context.CustomerItems == null)
            {
                return NotFound();
            }
            var customeritemdto = await _context.CustomerItems.FindAsync(id);

            if (customeritemdto != null)
            {
                CustomerItemDTO = customeritemdto;
                _context.CustomerItems.Remove(CustomerItemDTO);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
