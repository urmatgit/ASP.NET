using Mapster;
using PromoCodeFactory.Core.Domain.Administration;
using PromoCodeFactory.Core.Domain.PromoCodeManagement;
using PromoCodeFactory.WebHost.Models;

namespace PromoCodeFactory.WebHost.Mapping
{
    public class CustomerMappingConfig : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            //customer
            config.NewConfig<CreateOrEditCustomerRequest, Customer>();
            config.NewConfig<Customer, CustomerResponse>();

            //Employee
            config.NewConfig<Employee, EmployeeResponse>();
            config.NewConfig<Employee, EmployeeShortResponse>();

            //PromoCode
            config.NewConfig<GivePromoCodeRequest, PromoCode>();
            config.NewConfig<PromoCode, PromoCodeShortResponse>();
            //role
            config.NewConfig<Role, RoleItemResponse>();

            //Preference
            config.NewConfig<Preference, PreferenceResponse>();
            config.NewConfig<CustomerPreference, CustomerPreferenceResponse>();
        }
    }
}
