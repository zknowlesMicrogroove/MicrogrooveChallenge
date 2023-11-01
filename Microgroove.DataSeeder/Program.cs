using MicrogrooveChallenge.Data;
using MicrogrooveChallenge.Data.Models;
using Microsoft.EntityFrameworkCore;

Console.WriteLine("Seeding Records.");

var context = new CustomerContextFactory().CreateDbContext(new string[0]);
context.Database.Migrate();

var random = new Random();

var customers = new List<Customer>();

Console.WriteLine("Creating customers.");
for (int i = 0; i < 500; i++)
{
    customers.Add(new Customer()
    {
        CustomerId = Guid.NewGuid(),
        FullName = $"Seeded Persion{i}",
        DateOfBirth = DateOnly.FromDateTime(DateTime.Now.AddYears(-random.Next(0, 75)))
    });
}

Console.WriteLine("Saving customers to database.");
context.Customers.AddRange(customers);
context.SaveChanges();

var customer = context.Customers.FirstOrDefault();

Console.WriteLine("Customer seeding completed.");
Console.WriteLine($"{customer.FullName}");

Console.WriteLine("Press any key to end.");
Console.ReadKey();