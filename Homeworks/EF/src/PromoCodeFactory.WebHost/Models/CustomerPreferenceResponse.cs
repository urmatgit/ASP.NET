using PromoCodeFactory.Core.Domain.PromoCodeManagement;
using System;

namespace PromoCodeFactory.WebHost.Models
{
    public class CustomerPreferenceResponse
    {
        public Guid CustomerId { get; set; }
        public virtual CustomerResponse Customer { get; set; }
        public Guid PreferenceId { get; set; }
        public virtual PreferenceResponse Preference { get; set; }
    }
}
