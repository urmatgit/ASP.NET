using MongoDB.Bson;
using System;

namespace Pcf.GivingToCustomer.WebHost.Models
{
    public class PreferenceResponse
    {
        public ObjectId Id { get; set; }
        
        public string Name { get; set; }
    }
}