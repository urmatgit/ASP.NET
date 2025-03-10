using System;
using System.Collections.Generic;

namespace PromoCodeFactory.WebHost.Models
{
    public record UpdateEmployeeRequest(Guid id, string firstName, string lastName, string email,  int appliedPromocodesCount);
    
}
