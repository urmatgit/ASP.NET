using System;

namespace PromoCodeFactory.WebHost.Models
{
    public record UpdateRoleRequest(Guid id, string name, string description);
    
}
