using System;
using System.Collections.Generic;
using System.Linq;
using MongoDB.Bson;
using Pcf.GivingToCustomer.Core.Domain;

namespace Pcf.GivingToCustomer.DataAccess.Data
{
    public static class FakeDataFactory
    {
        
        public static List<Preference> Preferences => new List<Preference>()
        {
            new Preference()
            {
                Id =ObjectId.GenerateNewId(), // Parse("ef7f299f-92d7-459f-896e-078ed53ef99c"),
                Name = "Театр",
            },
            new Preference()
            {
                Id = ObjectId.GenerateNewId(), //Parse("c4bda62e-fc74-4256-a956-4760b3858cbd"),
                Name = "Семья",
            },
            new Preference()
            {
                Id =ObjectId.GenerateNewId(), //Parse("76324c47-68d2-472d-abb8-33cfa8cc0c84"),
                Name = "Дети",
            }
        };

        public static List<Customer> Customers
        {
            get
            {
                var customerId = ObjectId.GenerateNewId();// Guid.Parse("a6c8c6b1-4349-45b0-ab31-244740aaf0f0");
                var customers = new List<Customer>()
                {
                    new Customer()
                    {
                        Id = customerId,
                        Email = "ivan_sergeev@mail.ru",
                        FirstName = "Иван",
                        LastName = "Петров",
                        Preferences = new List<CustomerPreference>()
                        {
                            new CustomerPreference()
                            {
                                CustomerId = customerId,
                                PreferenceId =Preferences.FirstOrDefault(x=>x.Name=="Дети").Id//  Guid.Parse("76324c47-68d2-472d-abb8-33cfa8cc0c84")
                            }
                            //,
                            //new CustomerPreference()
                            //{
                            //    CustomerId = customerId,
                            //    PreferenceId =Preferences.FirstOrDefault(x=>x.Name=="Театр").Id//  Guid.Parse("ef7f299f-92d7-459f-896e-078ed53ef99c")
                            //}
                        }
                    }
                };

                return customers;
            }
        }
    }
}