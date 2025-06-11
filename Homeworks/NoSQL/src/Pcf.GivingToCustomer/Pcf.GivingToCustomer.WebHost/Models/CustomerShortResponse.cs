using MongoDB.Bson;
using System;

namespace Pcf.GivingToCustomer.WebHost.Models
{
    public class CustomerShortResponse
    {
        public ObjectId Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
    }
}