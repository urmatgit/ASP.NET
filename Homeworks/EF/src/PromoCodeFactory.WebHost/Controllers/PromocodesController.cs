using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MapsterMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PromoCodeFactory.Core.Abstractions.Repositories;
using PromoCodeFactory.Core.Domain.Administration;
using PromoCodeFactory.Core.Domain.PromoCodeManagement;
using PromoCodeFactory.DataAccess.Data;
using PromoCodeFactory.WebHost.Models;

namespace PromoCodeFactory.WebHost.Controllers
{
    /// <summary>
    /// Промокоды
    /// </summary>
    [ApiController]
    [Route("api/v1/[controller]")]
    public class PromocodesController
        : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IRepository<PromoCode> _promoCodeRepository;
        private readonly IRepository<Preference> _preferenceRepository;
        private readonly IRepository<Customer> _customerRepository;
        //временно!  для создание запроса
        private readonly EfDataContext _efDataContext;
        public PromocodesController(IRepository<PromoCode> repository, IRepository<Preference> preferenceRepository, IRepository<Customer> customerRepository, EfDataContext efDataContext, IMapper mapper)
        {
            _promoCodeRepository = repository;
            _mapper = mapper;
            _preferenceRepository = preferenceRepository;
            _customerRepository = customerRepository;

            _efDataContext = efDataContext;
        }
        /// <summary>
        /// Получить все промокоды
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<List<PromoCodeShortResponse>>> GetPromocodesAsync()
        {
            // Получить все промокоды 
            var promoCodes = await _promoCodeRepository.GetAllAsync();
            var promoCodesResponses = _mapper.Map<List<PromoCodeShortResponse>>(promoCodes);
            return Ok(promoCodesResponses);
        }

        /// <summary>
        /// Создать промокод и выдать его клиентам с указанным предпочтением
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> GivePromoCodesToCustomersWithPreferenceAsync(GivePromoCodeRequest request)
        {
            // Создать промокод и выдать его клиентам с указанным предпочтением

            var promoCodes = await _promoCodeRepository.GetAllAsync();
            if (promoCodes.Any(x => x.Code == request.PromoCode))
                return BadRequest($"Промокод '{request.PromoCode}' уже существует!");

            var promoCode = _mapper.Map<PromoCode>(request);
            // промокод добавлятся только одному клиенту
            var customer = await _efDataContext.Set<Customer>()
                .Include(x => x.Preferences)
                .ThenInclude(p => p.Preference)
                .Where(c => c.Preferences.Any(x => x.Preference.Name == request.Preference))
                .FirstOrDefaultAsync();
            promoCode.Customer = customer;

            var preference = await _efDataContext.Set<Preference>()
                .FirstOrDefaultAsync(x => x.Name == request.Preference);

            promoCode.Preference = preference;
            var employee = await _efDataContext.Set<Employee>()
                .FirstOrDefaultAsync(x => x.FirstName == request.PartnerName);

            promoCode.PartnerManager = employee;





            promoCode.BeginDate = DateTime.Now;
            promoCode.EndDate = DateTime.Now.AddDays(30);
            var result = await _promoCodeRepository.CreateAsync(promoCode);
            await _promoCodeRepository.SaveChangesAsync();

            return Ok(result);
        }
    }
}