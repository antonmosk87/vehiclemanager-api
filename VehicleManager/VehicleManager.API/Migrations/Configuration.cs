namespace VehicleManager.API.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using VehicleManager.API.Models;

    internal sealed class Configuration : DbMigrationsConfiguration<VehicleManager.API.Data.VehicleManagerDataContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(VehicleManager.API.Data.VehicleManagerDataContext context)
        {
            string[] colors = new string[] { "White", "Black", "Green", "Blue", "Red", "Purple", "Yellow", "Brown" };
            string[] makes = new string[] { "Honda", "Toyota", "BMW", "Mercedes", "Ford", "Hyundai", "Kia", "Audi", "GM", "Lexus" };
            string[] models = new string[] { "Civic", "Camry", "330", "S55", "Focus", "Genesis", "Optima", "A4", "Denali", "IS350" };
            string[] vehicleTypes = new string[] { "Sedan", "Coupe", "SUV", "Crossover" };


            if (context.Customers.Count() == 0)
            {
                for (int i = 0; i < 25; i++)
                {
                    context.Customers.Add(new Models.Customer
                    {
                        EmailAddress = Faker.InternetFaker.Email(),
                        DateOfBirth = Faker.DateTimeFaker.BirthDay(),
                        FirstName = Faker.NameFaker.FirstName(),
                        LastName = Faker.NameFaker.LastName(),
                        Telephone = Faker.PhoneFaker.Phone()
                    });
                }

                context.SaveChanges();

            }

            if (context.Vehicles.Count() == 0)
            {
                for (int i = 0; i < 25; i++)
                {
                    context.Vehicles.Add(new Models.Vehicle
                    {
                        Make = Faker.ArrayFaker.SelectFrom(makes),
                        Model = Faker.ArrayFaker.SelectFrom(models),
                        Color = Faker.ArrayFaker.SelectFrom(colors),
                        RetailPrice = Faker.NumberFaker.Number(15000, 100000),
                        VehicleType = Faker.ArrayFaker.SelectFrom(vehicleTypes),
                        Year = Faker.DateTimeFaker.DateTime().Year
                    });
                }

                context.SaveChanges();
            }

            if (context.Sales.Count() == 0)
            {
                for (int i = 0; i < 25; i++)
                {
                    var vehicle = context.Vehicles.Find(Faker.NumberFaker.Number(1, 25));
                    var invoiceDate = Faker.DateTimeFaker.DateTime();

                    context.Sales.Add(new Sale
                    {
                        Customer = context.Customers.Find(Faker.NumberFaker.Number(1, 25)),
                        Vehicle = vehicle,
                        InvoiceDate = invoiceDate,
                        SalePrice = vehicle.RetailPrice,
                        PaymentReceived = invoiceDate.AddDays(Faker.NumberFaker.Number(1,14)),
                    });
                }
            }
            
        }
    }
}
