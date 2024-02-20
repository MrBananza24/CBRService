using CBRService.Models;
using CBRService.Services;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;

namespace CBRService.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CurrencyController : ControllerBase
    {
        private readonly CurrencyService _currencyService;

        public CurrencyController(CurrencyService currencyService)
        {
            _currencyService = currencyService;
        }

        /// <summary>
        /// Возвращает список курсов валют с возможностью пагинации
        /// </summary>
        /// <param name="page">Номер страницы</param>
        [Route("~/currencies")]
        [HttpGet]
        public async Task<ActionResult<List<Valute>>> GetCurrencies([FromQuery] int page = 1)
        {
            try
            {
            var currencies = await _currencyService.GetCurrenciesAsync(page);
            return currencies is null ? NotFound() : Ok(currencies);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// <summary>
        /// Возвращает курс валюты для переданного идентификатора валюты
        /// </summary>
        /// <param name="code">Трехбуквенный идентификатор валюты</param>
        [HttpGet("{code}")]
        public async Task<ActionResult<Valute>> GetCurrency(string code)
        {
            try
            {
                var currency = await _currencyService.GetCurrencyAsync(code);
                return currency is null ? NotFound() : Ok(currency);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
