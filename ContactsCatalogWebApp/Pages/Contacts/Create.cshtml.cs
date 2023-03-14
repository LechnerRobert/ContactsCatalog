using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ContactsCatalog.Data;
using ContactsCatalog.Models;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.CodeAnalysis.Elfie.Diagnostics;

namespace ContactsCatalogWebApp.Pages.Contacts
{
    public class CreateModel : PageModel
    {
        private readonly ContactsCatalog.Data.CustomerContext _context;

        public CreateModel(ContactsCatalog.Data.CustomerContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            customerID = long.Parse(Request.Query["customerID"]);
            
            var customerItem =  _context.CustomerItems.FirstOrDefault(m => m.Id == customerID);
            if (customerItem == null)
            {
                return NotFound();
            }
            CustomerItemDTO = CustomerContext.CustomerToDTO(customerItem);
//          ViewData["CustomerId"] = new SelectList(_context.CustomerItems, "Id", "Id");
            

            return Page();
        }

        public CustomerItemDTO CustomerItemDTO { get; set; } = default!;

        [BindProperty]
        public long customerID { get; set; }

        [BindProperty]
        public Contact Contact { get; set; } = default!;

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            Contact.CustomerId = customerID;

          if (!ModelState.IsValid || _context.ContactItems == null || Contact == null)
            {
              var errors = ModelState
               .Where(x => x.Value?.Errors.Count > 0)
               .Select(x => new { x.Key, x.Value?.Errors })
               .ToArray();
                
                return Page();
            }
            
            _context.ContactItems.Add(Contact);
            await _context.SaveChangesAsync();

            return RedirectToPage("../Customers/Details", new { id = Contact.CustomerId.ToString()});
        }
    }
}
