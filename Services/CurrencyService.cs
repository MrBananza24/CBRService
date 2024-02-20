using CBRService.Models;
using Microsoft.Extensions.Caching.Memory;
using System.Data;

namespace CBRService.Services
{
    public class CurrencyService
    {
        private static readonly int _pageSize = 10;
        private readonly IMemoryCache _memoryCache;
        private readonly HttpClient   _httpClient;

        public CurrencyService(IMemoryCache memoryCache, HttpClient httpClient)
        {
            _memoryCache = memoryCache;
            _httpClient = httpClient;
        }

        public async Task<List<Valute>?> GetCurrenciesAsync(int page)
        {
            if (page < 1)
                return null;
            var data = await GetDataAsync();
            if (data is null)
                return null;
            return data.Valute.Select(v => v.Value)
                              .Skip((page - 1) * _pageSize)
                              .Take(_pageSize)
                              .ToList();
        }

        public async Task<Valute?> GetCurrencyAsync(string code)
        {
            var data = await GetDataAsync();
            if (data is null)
                return null;
            data.Valute.TryGetValue(code, out Valute? valute);
            return valute;
        }

        private async Task<CurrencyData?> GetDataAsync()
        {
            var success = _memoryCache.TryGetValue("dictionary", out CurrencyData? data);
            if (!success || DateTime.Now.Date != data?.Timestamp.Date)
            {
                data = await _httpClient.GetFromJsonAsync<CurrencyData?>(string.Empty);
                if (data is not null)
                    _memoryCache.Set("dictionary", data, TimeSpan.FromHours(1.0f));
            }
            return data;
        }

    }
}
