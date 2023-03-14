using Microsoft.EntityFrameworkCore;
using ContactsCatalog.Models;
using System;
using System.Collections.Generic;
using System.Configuration;

namespace ContactsCatalog.Data
{
    public class CustomerContext : DbContext
    {
        public CustomerContext(DbContextOptions<CustomerContext> options)
               : base(options)
        {
        }
        
        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            // connect to sqlite database
//          options.UseSqlite(Configuration.GetConnectionString("WebApiDatabase"));
        }

        public DbSet<CustomerItem> CustomerItems { get; set; } = null!;
        public DbSet<Contact> ContactItems { get; set; } = null!;

        public static CustomerItemDTO CustomerToDTO(CustomerItem todoItem) =>
           new CustomerItemDTO
        {
          Id = todoItem.Id,
          Name = todoItem.Name,
          Number = todoItem.Number,
          Contacts = todoItem.Contacts
        };
       
    }
}
