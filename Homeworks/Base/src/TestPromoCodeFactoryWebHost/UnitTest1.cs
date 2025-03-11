using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OutputCaching;
using Moq;
using PromoCodeFactory.Core.Abstractions.Repositories;
using PromoCodeFactory.Core.Domain.Administration;
using PromoCodeFactory.DataAccess.Data;
using PromoCodeFactory.DataAccess.Repositories;
using PromoCodeFactory.WebHost.Common;
using PromoCodeFactory.WebHost.Controllers;
using PromoCodeFactory.WebHost.Models;
using System.Net.NetworkInformation;

namespace TestPromoCodeFactoryWebHost
{
    public class UnitTest1
    {
        private readonly InMemoryRepository<Employee> _employeeRepository;
        private readonly EmployeesController _employeesController;
        private readonly Mock<IRepository<Employee>> _mockService; 
        public UnitTest1()
        {
            _employeeRepository = new InMemoryRepository<Employee>(FakeDataFactory.Employees);
            _mockService = new Mock<IRepository<Employee>>();
            _employeesController=new EmployeesController(_mockService.Object);
        }
        [Fact]
        public async Task Test_GetEmployerList()
        {
            // Arrange
           _mockService.Setup(service=>service.GetAllAsync()).Returns(_employeeRepository.GetAllAsync());
            

            // Act
            var result = await _employeesController.GetEmployeesAsync();

            // Assert
            
            Assert.Equal(2, result.Count());
        }
        [Fact]
        public async Task Test_CreateEmployer()
        {
            // Arrange
            var createEmployeeRequest = new CreateEmployeeRequest("Test", "Testov", "test@gmail.com", 100);
            var employee = Helpers.ConvertorCreateRequestToDomain(createEmployeeRequest);
            _mockService.Setup(service => service.CreateAsync(It.IsAny<Employee>()))
                .ReturnsAsync(employee);
            //_mockService.Setup(service => service.CreateAsync()).Returns(_employeeRepository.CreateAsync);


            // Act
            var result = await _employeesController.CreateAsync(createEmployeeRequest);
            var okRestult = result.Result as ObjectResult;
            // Assert
            Assert.NotNull(okRestult);
            Assert.Equal(200, okRestult.StatusCode);
            Assert.Equal(employee.Id, (okRestult.Value as EmployeeResponse).Id);
        }
        [Fact]
        public async Task Test_UpdateEmployee()
        {
            // Arrange
            var firstEmployee = _employeeRepository.GetAllAsync().Result.FirstOrDefault();
            UpdateEmployeeRequest request = new UpdateEmployeeRequest(firstEmployee.Id, "NewName", firstEmployee.LastName, firstEmployee.Email, 99);
            var employee = Helpers.ConvertorUpdateRequestToDomain(request);
            _mockService.Setup(service => service.UpdateAsync(It.IsAny<Employee>()))
                .ReturnsAsync(employee);

            // Act
            var result = await _employeesController.UpdateAsync(request);
            var okRestult = result.Result as ObjectResult;
            // Assert
            Assert.NotNull(okRestult);
            Assert.Equal(200, okRestult.StatusCode);
            Assert.Equal(employee.FullName, (okRestult.Value as EmployeeResponse).FullName);
        }
        [Fact]
        public async Task Test_DeleteEpmployee()
        {
            // Arrange
            var firstEmployee = _employeeRepository.GetAllAsync().Result.FirstOrDefault();
            var id = firstEmployee.Id;

            _mockService.Setup(service => service.DeleteAsync(It.IsAny<Guid>())).ReturnsAsync(true);
                

            // Act
            var result = await _employeesController.DeleteAsync(id);
            var okRestult = result.Result as ObjectResult; 
            // Assert
            Assert.NotNull(okRestult);

            Assert.True((okRestult.Value as bool?)==true);
        }
    }
}