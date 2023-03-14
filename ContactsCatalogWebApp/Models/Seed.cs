using Microsoft.EntityFrameworkCore;
using ContactsCatalog.Data;
using ContactsCatalog.Models;

namespace ContactsCatalogWebApp.Models;



public static class SeedData
{
    public static void Initialize(IServiceProvider serviceProvider)
    {
        using (var context = new CustomerContext(
            serviceProvider.GetRequiredService<
                DbContextOptions<CustomerContext>>()))
        {
            if (context == null || context.CustomerItems == null)
            {
                throw new ArgumentNullException("Null CustomerItemsContext");
            }

            // Look for any Customers.
            if (context.CustomerItems.Any())
            {
                return;   // DB has been seeded
            }

            context.CustomerItems.AddRange(
                new CustomerItem
                {
                    Name = "Hans Müller",
                    Number = "230001"                    
                },

                new CustomerItem
                {
                    Name = "Klaus Müller",
                    Number = "230002"
                },

                new CustomerItem
                {
                    Name = "Sepp Oberladstätter",
                    Number = "230011"
                },

                new CustomerItem
                {
                    Name = "Tom Tunicht",
                    Number = "230041"
                },

                new CustomerItem
                {
                    Name = "Ester Maiyer",
                    Number = "230021"
                }
            );
            context.SaveChanges();
        }
    }
}