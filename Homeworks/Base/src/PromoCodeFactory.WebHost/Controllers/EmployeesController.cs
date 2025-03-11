using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PromoCodeFactory.Core.Abstractions.Repositories;
using PromoCodeFactory.Core.Domain.Administration;
using PromoCodeFactory.WebHost.Models;

namespace PromoCodeFactory.WebHost.Controllers
{
    /// <summary>
    /// Сотрудники
    /// </summary>
    [ApiController]
    [Route("api/v1/[controller]")]
    public class EmployeesController : ControllerBase
    {
        private readonly IRepository<Employee> _employeeRepository;

        public EmployeesController(IRepository<Employee> employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        /// <summary>
        /// Получить данные всех сотрудников
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<List<EmployeeShortResponse>> GetEmployeesAsync()
        {
            var employees = await _employeeRepository.GetAllAsync();

            var employeesModelList = employees.Select(x =>
                new EmployeeShortResponse()
                {
                    Id = x.Id,
                    Email = x.Email,
                    FullName = x.FullName,
                }).ToList();

            return  employeesModelList;
        }

        /// <summary>
        /// Получить данные сотрудника по Id
        /// </summary>
        /// <returns></returns>
        [HttpGet("{id:guid}")]
        public async Task<ActionResult<EmployeeResponse>> GetEmployeeByIdAsync(Guid id)
        {
            var employee = await _employeeRepository.GetByIdAsync(id);

            if (employee == null)
                return NotFound();

            var employeeModel = ConvertorToResponse(employee);

            return Ok(employeeModel);
        }
        /// <summary>
        /// Создать сотрутника
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<EmployeeResponse>> CreateAsync(CreateEmployeeRequest request){
            var employees = await _employeeRepository.GetAllAsync();
            if (employees.SingleOrDefault(x => x.Email.Equals(request.email)) is not null)
            {
                return BadRequest("Employer with this Email already exists.");
            }
            var employee = new Employee
            {
                Id=Guid.NewGuid(),
                FirstName = request.firstName,
                LastName = request.lastName,
                Email = request.email,
                AppliedPromocodesCount = request.appliedPromocodesCount
                
            };
            var result = await _employeeRepository.CreateAsync(employee);
            return Ok(ConvertorToResponse(result));
        }
        /// <summary>
        /// Изменение данных сотрудника
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<ActionResult<EmployeeResponse>> UpdateAsync(UpdateEmployeeRequest request)
        {
            var result = await _employeeRepository.UpdateAsync(ConvertorToDomain(request));

           return Ok(ConvertorToResponse(result));
        }
        /// <summary>
        /// Удаляем
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id:guid}")]
        public async Task<ActionResult> DeleteAsync(Guid id)
        {
            var result= await _employeeRepository.DeleteAsync(id);
            if (result)
            {
                return Ok("Employee deleted");
            }
            else
                return NotFound($"Employee with id ({id}) not found");
        }
        /// <summary>
        /// TODO Потом  использовать Mapper 
        /// </summary>
        /// <param name="employee"></param>
        /// <returns></returns>
        private Employee ConvertorToDomain(UpdateEmployeeRequest employee)
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

        private EmployeeResponse ConvertorToResponse(Employee employee) {
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