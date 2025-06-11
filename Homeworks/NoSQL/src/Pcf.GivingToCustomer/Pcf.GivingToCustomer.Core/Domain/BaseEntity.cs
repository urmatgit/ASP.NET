using MongoDB.Bson;
using System;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Pcf.GivingToCustomer.Core.Domain
{
    public class BaseEntity
    {
        public ObjectId Id { get; set; }
    }
}