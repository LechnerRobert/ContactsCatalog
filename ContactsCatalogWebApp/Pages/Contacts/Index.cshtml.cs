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
    public class IndexModel : PageModel
    {
        private readonly ContactsCatalog.Data.CustomerContext _context;

        public IndexModel(ContactsCatalog.Data.CustomerContext context)
        {
            _context = context;
        }

        public IList<Contact> Contact { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.ContactItems != null)
            {
                Contact = await _context.ContactItems
                .Include(c => c.Customer).ToListAsync();
            }
        }
    }
}
