using MongoDB.Bson;
using System;

namespace Pcf.GivingToCustomer.Core.Domain
{
    public class CustomerPreference
    {
        public ObjectId CustomerId { get; set; }
        public virtual Customer Customer { get; set; }

        public ObjectId PreferenceId { get; set; }
        public virtual Preference Preference { get; set; }
    }
}