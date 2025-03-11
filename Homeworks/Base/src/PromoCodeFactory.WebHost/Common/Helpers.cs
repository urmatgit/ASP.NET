using PromoCodeFactory.Core.Domain.Administration;
using PromoCodeFactory.WebHost.Models;
using System;

namespace PromoCodeFactory.WebHost.Common
{
    public static class Helpers
    {
        /// <summary>
        /// TODO Потом  использовать Mapper 
        /// </summary>
        /// <param name="employee"></param>
        /// <returns></returns>
        public static Employee ConvertorUpdateRequestToDomain(UpdateEmployeeRequest employee)
        {
            var employeeModel = new Employee()
            {
                Id = employee.id,
                Email = employee.email,
                FirstName = employee.firstName,
                LastName = employee.lastName,

                AppliedPromocodesCount = employee.appliedPromocodesCount
            };

            return employeeModel;
        }

        public static Employee ConvertorCreateRequestToDomain(CreateEmployeeRequest employee)
        {
            var employeeModel = new Employee()
            {
                Id = Guid.NewGuid(),
                Email = employee.email,
                FirstName = employee.firstName,
                LastName = employee.lastName,

                AppliedPromocodesCount = employee.appliedPromocodesCount
            };

            return employeeModel;
        }
        public static EmployeeResponse ConvertorDomainToResponse(Employee employee)
        {
            var employeeModel = new EmployeeResponse()
            {
                Id = employee.Id,
                Email = employee.Email,

                FullName = employee.FullName,
                AppliedPromocodesCount = employee.AppliedPromocodesCount
            };

            return employeeModel;
        }
    }
}
