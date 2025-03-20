using Mapster;
using Microsoft.EntityFrameworkCore.Sqlite.Storage.Internal;
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
            config.NewConfig<GivePromoCodeRequest, PromoCode>()
                .Map(d => d.Code, src => src.PromoCode)
                .Map(d => d.Preference,src=>new Preference { Name=src.Preference})
                .Map(d => d.PartnerName,src=>new Employee { FirstName=src.PartnerName});


            config.NewConfig<PromoCode, PromoCodeShortResponse>()
                .Map(d => d.BeginDate, src => src.BeginDate.ToString("dd.MM.yyyy hh:mm:ss"))
                .Map(d => d.EndDate, src => src.EndDate.ToString("dd.MM.yyyy hh:mm:ss"));
            //role
            config.NewConfig<Role, RoleItemResponse>();

            //Preference
            config.NewConfig<Preference, PreferenceResponse>();
            config.NewConfig<CustomerPreference, CustomerPreferenceResponse>();
        }
    }
}
