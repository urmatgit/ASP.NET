using MongoDB.Bson;
using System;

namespace Pcf.GivingToCustomer.WebHost.Models
{
    public class GivePromoCodeRequest
    {
        public string ServiceInfo { get; set; }

        public ObjectId  PartnerId { get; set; }

        public ObjectId PromoCodeId { get; set; }
        
        public string PromoCode { get; set; }

        public ObjectId PreferenceId { get; set; }

        public string BeginDate { get; set; }

        public string EndDate { get; set; }
    }
}