using System.Collections.Generic;

namespace PromoCodeFactory.WebHost.Models
{
    public record CreateEmployeeRequest(string firstName,string lastName,string email,int appliedPromocodesCount);
}
