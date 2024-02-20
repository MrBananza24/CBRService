using CBRService.Models;
using CBRService.Services;
using Microsoft.AspNetCore.Mvc;

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

        [Route("~/currencies")]
        [HttpGet]
        public async Task<ActionResult<List<Valute>>> GetCurrencies([FromQuery] int page)
        {
            var currencies = await _currencyService.GetCurrenciesAsync(page);
            return currencies is null ? NotFound() : Ok(currencies);
        }

        [HttpGet("{code}")]
        public async Task<ActionResult<Valute>> GetCurrency(string code)
        {
            var currency = await _currencyService.GetCurrencyAsync(code);
            return currency is null ? NotFound() : Ok(currency);
        }
    }
}
