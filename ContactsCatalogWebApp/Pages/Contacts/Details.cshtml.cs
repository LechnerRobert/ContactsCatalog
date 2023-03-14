using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ContactsCatalog.Data;
using ContactsCatalog.Models;

namespace ContactsCatalogWebApp.Pages.Contacts
{
    public class DetailsModel : PageModel
    {
        private readonly ContactsCatalog.Data.CustomerContext _context;

        public DetailsModel(ContactsCatalog.Data.CustomerContext context)
        {
            _context = context;
        }

      public Contact Contact { get; set; } = default!; 

        public async Task<IActionResult> OnGetAsync(long? id)
        {
            if (id == null || _context.ContactItems == null)
            {
                return NotFound();
            }

            var contact = await _context.ContactItems.Include(c => c.Customer).FirstOrDefaultAsync(m => m.Id == id);
            if (contact == null)
            {
                return NotFound();
            }
            else 
            {
                Contact = contact;
            }
            return Page();
        }
    }
}
