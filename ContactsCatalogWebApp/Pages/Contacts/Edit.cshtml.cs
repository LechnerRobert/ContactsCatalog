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

namespace ContactsCatalogWebApp.Pages.Contacts
{
    public class EditModel : PageModel
    {
        private readonly ContactsCatalog.Data.CustomerContext _context;

        public EditModel(ContactsCatalog.Data.CustomerContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Contact Contact { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(long? id)
        {
            if (id == null || _context.ContactItems == null)
            {
                return NotFound();
            }

            var contact =  await _context.ContactItems.Include(c => c.Customer).FirstOrDefaultAsync(m => m.Id == id);
            if (contact == null)
            {
                return NotFound();
            }
            Contact = contact;

            ViewData["CustomerId"] = new SelectList(_context.CustomerItems, "Id", "Id");
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }


            var contactItem = await _context.ContactItems.FindAsync(Contact.Id);
            if (contactItem == null)
            {
                return NotFound();
            }


            if (await TryUpdateModelAsync<Contact>(
              contactItem,
              "Contact",
              s => s.PhoneNumber, s => s.FirstName, s => s.LastName, s => s.EMail))
            { }
            else
            {
                return Page();
            }

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ContactExists(Contact.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("../Customers/Details", new { id = contactItem.CustomerId.ToString() });
        }

        private bool ContactExists(long id)
        {
          return (_context.ContactItems?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
