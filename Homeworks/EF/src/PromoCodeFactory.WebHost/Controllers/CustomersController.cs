using MapsterMapper;
using Microsoft.AspNetCore.Mvc;
using PromoCodeFactory.Core.Abstractions.Repositories;
using PromoCodeFactory.Core.Domain.PromoCodeManagement;
using PromoCodeFactory.WebHost.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PromoCodeFactory.WebHost.Controllers
{
    /// <summary>
    /// Клиенты
    /// </summary>
    [ApiController]
    [Route("api/v1/[controller]")]
    public class CustomersController
        : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IRepository<Customer> _customerRepository;
        public CustomersController(IRepository<Customer> repository,IMapper mapper)
        {
            _customerRepository = repository;
            _mapper = mapper;
        }
        /// <summary>
        /// Полный список Покупателей
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<CustomerShortResponse>> GetCustomersAsync()
        {
            // Добавить получение списка клиентов
            var customers=await _customerRepository.GetAllAsync(); 
            var customerResponses=_mapper.Map<List<CustomerResponse>>(customers);
            return Ok( customerResponses);
        }
        /// <summary>
        /// получаем покупателья через id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<CustomerResponse>> GetCustomerAsync(Guid id)
        {
            // Добавить получение клиента вместе с выданными ему промомкодами
            var customer = await _customerRepository.GetByIdAsync(id);
            var customerResponse = _mapper.Map<CustomerResponse>(customer);
            return Ok(customerResponse);
        }
        /// <summary>
        /// Добавление покупателя
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> CreateCustomerAsync(CreateOrEditCustomerRequest request)
        {
            // Добавить создание нового клиента вместе с его предпочтениями

            var customer = _mapper.Map<Customer>(request);
            var result=await _customerRepository.CreateAsync(customer);
            await _customerRepository.SaveChangesAsync();
            var response=_mapper.Map<CustomerResponse>(result);
            return Ok(response);
        }
        /// <summary>
        /// Редактировать 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> EditCustomersAsync(Guid id, CreateOrEditCustomerRequest request)
        {
            // Обновить данные клиента вместе с его предпочтениями
            var customer = _mapper.Map<Customer>(request);
            customer.Id = id;
            var result = await _customerRepository.UpdateAsync(customer);
            await _customerRepository.SaveChangesAsync();
            var response = _mapper.Map<CustomerResponse>(result);
            return Ok(response);
        }
        /// <summary>
        /// Удаление
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        public async Task<IActionResult> DeleteCustomer(Guid id)
        {
            var result = await _customerRepository.DeleteAsync(id);
            await _customerRepository.SaveChangesAsync();
            return Ok(result);
        }
    }
}