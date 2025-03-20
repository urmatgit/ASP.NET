using MapsterMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PromoCodeFactory.Core.Abstractions.Repositories;
using PromoCodeFactory.Core.Domain.PromoCodeManagement;
using PromoCodeFactory.WebHost.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;

namespace PromoCodeFactory.WebHost.Controllers
{
    /// <summary>
    /// Предпочтение
    /// </summary>
    [ApiController]
    [Route("api/v1/[controller]")]
    public class PreferenceController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IRepository<Preference> _preferenceRepository;
        public PreferenceController(IRepository<Preference> repository, IMapper mapper)
        {
            _preferenceRepository = repository;
            _mapper = mapper;
        }
        /// <summary>
        /// Полный список предпочтений 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<CustomerShortResponse>> GetCustomersAsync()
        {
            // Добавить получение списка предпочтений 
            var customers = await _preferenceRepository.GetAllAsync();
            var customerResponses = _mapper.Map<List<PreferenceResponse>>(customers);
            return Ok(customerResponses);
        }
        /// <summary>
        /// получаем предпочтение  через id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<PreferenceResponse>> GetCustomerAsync(Guid id)
        {
            // Добавить получение предпочтение  
            var customer = await _preferenceRepository.GetByIdAsync(id);
            var customerResponse = _mapper.Map<PreferenceResponse>(customer);
            return Ok(customerResponse);
        }
        /// <summary>
        /// Добавление предпочтение 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> CreateCustomerAsync(CreateOrEditCustomerRequest request)
        {
            // Добавить создание нового предпочтение 

            var customer = _mapper.Map<Preference>(request);
            var result = await _preferenceRepository.CreateAsync(customer);
            var response = _mapper.Map<PreferenceResponse>(result);
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
            // Обновить данные предпочтение  
            var customer = _mapper.Map<Preference>(request);
            customer.Id = id;
            var result = await _preferenceRepository.UpdateAsync(customer);
            var response = _mapper.Map<PreferenceResponse>(result);
            return Ok(response);
        }
        /// <summary>
        /// Удаление предпочтение 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        public async Task<IActionResult> DeleteCustomer(Guid id)
        {
            var result = await _preferenceRepository.DeleteAsync(id);
            return Ok(result);
        }
    }
}
