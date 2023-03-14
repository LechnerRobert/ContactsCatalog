using Microsoft.EntityFrameworkCore;
using ContactsCatalog.Data;
using ContactsCatalog.Models;
using Microsoft.EntityFrameworkCore.Storage;

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


            using (IDbContextTransaction transaction = context.Database.BeginTransaction())
            {
                try
                {

                    var cu = new CustomerItem
                    {
                        Name = "Firma Müller",
                        Number = "230001"
                    };
                    context.CustomerItems.Add(cu);
                    context.SaveChanges();


                    context.ContactItems.AddRange(
                        new Contact
                        {
                            CustomerId = cu.Id,
                            FirstName = "Hans",
                            LastName = "Müller",
                            PhoneNumber = "+43 (699) 12332123",
                            EMail = "hm@examaple.com"
                        },
                        new Contact
                        {
                            CustomerId = cu.Id,
                            FirstName = "Lora",
                            LastName = "Müller",
                            PhoneNumber = "+43 (699) 123321233",
                            EMail = "lm@examaple.com"
                        }
                        );
                    context.SaveChanges();


                    context.CustomerItems.Add(
                        new CustomerItem
                        {
                            Name = "Firma Moser",
                            Number = "230002"
                        });
                    context.SaveChanges();

                    context.CustomerItems.Add(
                        new CustomerItem
                        {
                            Name = "Firma Oberladstätter",
                            Number = "230011"
                        });
                    context.SaveChanges();

                    context.CustomerItems.Add(
                        new CustomerItem
                        {
                            Name = "Firma Tunicht",
                            Number = "230041"
                        });
                    context.SaveChanges();

                    context.CustomerItems.Add(
                    new CustomerItem
                        {
                            Name = "Firma Maiyer",
                            Number = "230021"
                    });
                    context.SaveChanges();


                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    Console.WriteLine("Error occurred.");
                }
            }

        }
    }
}