using MongoDB.Bson;
using System;

namespace Pcf.GivingToCustomer.Core.Domain
{
    public class PromoCodeCustomer : BaseEntity
    {
        public ObjectId PromoCodeId { get; set; }
        public virtual PromoCode PromoCode { get; set; }

        public ObjectId CustomerId { get; set; }
        public virtual Customer Customer { get; set; }
    }
}
