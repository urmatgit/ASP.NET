using System.Collections.Generic;

namespace PromoCodeFactory.WebHost.Models
{
    public record CreateEmployeeRequest(string firstName,string lastName,string email,List<CreateRoleRequest> roles,int appliedPromocodesCount);
}
