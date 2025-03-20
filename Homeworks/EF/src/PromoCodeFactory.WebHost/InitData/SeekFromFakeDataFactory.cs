using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json.Bson;
using PromoCodeFactory.Core.Abstractions.Repositories;
using PromoCodeFactory.Core.Domain.Administration;
using PromoCodeFactory.Core.Domain.PromoCodeManagement;
using PromoCodeFactory.DataAccess.Data;
using PromoCodeFactory.DataAccess.Repositories;
using System;
using System.Data;
using System.Security.Cryptography.Xml;
using System.Threading.Tasks;

namespace PromoCodeFactory.WebHost.InitData
{
    public class SeekFromFakeDataFactory
    {
        private readonly IRepository<Employee> _employeeRepository;
        private readonly IRepository<Role> _roleRepository;
        private readonly IRepository<Preference> _preferenceRepository;
        private readonly IRepository<Customer> _customerRepository;

        public SeekFromFakeDataFactory(IServiceProvider serviceProvider )
        {

            var scope = serviceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope();
            
                _employeeRepository=scope.ServiceProvider.GetService<IRepository<Employee>>();
                _roleRepository=scope.ServiceProvider.GetService<IRepository<Role>>();
            _preferenceRepository= scope.ServiceProvider.GetService<IRepository<Preference>>();
            _customerRepository = scope.ServiceProvider.GetService<IRepository<Customer>>();

        }
        public async Task RoleDataSeek()
        {
            if (!await _roleRepository.AnyAsync()) { 
            foreach (var role in FakeDataFactory.Roles)
            {

                await _roleRepository.CreateAsync(role);
            }
            await _roleRepository.SaveChangesAsync();
        }
        }
        public async Task EmployeeDataSeek()
        {
            if (!await _employeeRepository.AnyAsync())
            {

                foreach (var employee in FakeDataFactory.Employees)
                {
                    employee.RoleId = employee.Role.Id;
                    await _employeeRepository.CreateAsync(employee);
                }
                await _employeeRepository.SaveChangesAsync();
            }   
        }

        public async Task ReferenceDataSeek()
        {
            if (!await _preferenceRepository.AnyAsync())
            {
                foreach (var preference in FakeDataFactory.Preferences)
                {

                    await _preferenceRepository.CreateAsync(preference);
                }
                await _preferenceRepository.SaveChangesAsync();
            }
        }
        public async Task CustomerDataSeek()
        {
            if (!await _customerRepository.AnyAsync())
            {
                foreach(var customer in FakeDataFactory.Customers)
                {
                    await _customerRepository.CreateAsync(customer);
                }
                await _customerRepository.SaveChangesAsync();
            }
        }
    }
}
