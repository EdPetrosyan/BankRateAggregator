using BankRateAggregator.Application.Interfaces;
using BankRateAggregator.Application.Services.Banks.Models;
using BankRateAggregator.Application.Services.Currency.Interfaces;
using BankRateAggregator.Application.Services.Currency.Models;
using BankRateAggregator.Domain.Entities.BankRates;
using HtmlAgilityPack;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace BankRateAggregator.Application.Services.Currency
{
    public class CurrencyParserService : ICurrencyParserService
    {
        private readonly IApplicationDbContext _dbContext;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ILogger<CurrencyParserService> _logger;
        public CurrencyParserService(IApplicationDbContext dbContext, IHttpClientFactory httpClientFactory, ILogger<CurrencyParserService> logger)
        {
            _dbContext = dbContext;
            _httpClientFactory = httpClientFactory;
            _logger = logger;
        }

        public async Task<List<Rate>?> WebScrappingAsync(string url, string xPath, int bankId, List<CurrencyIdValuePair> currencies, CancellationToken cancellationToken)
        {
            using var httpClient = _httpClientFactory.CreateClient();
            httpClient.BaseAddress = new Uri(url);

            try
            {
                var response = await httpClient.GetAsync(url, cancellationToken);

                if (!response.IsSuccessStatusCode)
                {
                    _logger.LogWarning($"Request to : {url} returned with StatusCode : {response.StatusCode}: Content {response.Content}");
                    return null;
                }

                var html = await response.Content.ReadAsStringAsync(cancellationToken);
                var htmlDocument = new HtmlDocument();
                htmlDocument.LoadHtml(html);

                var heading = htmlDocument.DocumentNode.SelectNodes(xpath: xPath).FirstOrDefault();
                if (heading != null)
                {
                    var list = heading.InnerHtml.Replace('\n', ' ').Replace('\t', ' ').Replace('<', ' ').Replace('>', ' ').Trim().Split(' ').Where(x => x != "").ToList();

                    var listCurrencies = GetCurrenciesFromHtml(list, currencies);
                    var rates = CunstructRateEntityModel(listCurrencies, bankId);

                    return rates;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred during web scraping.");
                return null;
            }
        }

        public async Task<List<Rate>?> ApiCallJsonAsync(BaseApiModel model, string url, int bankId, List<CurrencyIdValuePair> currencies, CancellationToken cancellationToken)
        {

            using var httpClient = _httpClientFactory.CreateClient();
            httpClient.BaseAddress = new Uri(url);
            try
            {
                var response = await httpClient.GetAsync(url, cancellationToken);

                if (!response.IsSuccessStatusCode)
                {
                    _logger.LogWarning($"Request to : {url} returned with StatusCode : {response.StatusCode}: Content {response.Content}");
                    return await Task.FromResult<List<Rate>?>(null);
                }

                string responseBody = await response.Content.ReadAsStringAsync(cancellationToken);

                return model.DeserializeObject(responseBody, currencies, bankId);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred during ApiCallJson");
            }
            return await Task.FromResult<List<Rate>?>(null);
        }

        public async Task<List<Rate>?> ApiCallXmlAsync(BaseApiXMLModel model, string url, int bankId, List<CurrencyIdValuePair> currencies, CancellationToken cancellationToken)
        {
            using HttpClient client = new();
            try
            {
                HttpResponseMessage response = await client.GetAsync(url, cancellationToken);

                if (!response.IsSuccessStatusCode)
                {
                    _logger.LogWarning($"Request to : {url} returned with StatusCode : {response.StatusCode}: Content {response.Content}");
                    return await Task.FromResult<List<Rate>?>(null);
                }

                using Stream responseStream = await response.Content.ReadAsStreamAsync(cancellationToken);


                return model.DeserializeObject(responseStream, currencies, bankId);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred during ApiCallXml");
            }
            return await Task.FromResult<List<Rate>?>(null); ;
        }


        public async Task<List<CurrencyIdValuePair>> GetCurrencies(CancellationToken cancellationToken)
        {
            var currencies = await _dbContext.Currencies
                 .Select(x => new CurrencyIdValuePair { Id = x.Id, Code = x.Code })
                 .ToListAsync(cancellationToken);

            var aliases = await _dbContext.Currencies
                .Where(x => x.Alias != null)
                .Select(x => new CurrencyIdValuePair { Id = x.Id, Code = x.Alias })
                 .ToListAsync(cancellationToken);

            return currencies.Concat(aliases).ToList();
        }

        #region Private Methods
        private static List<CurrencyResult> GetCurrenciesFromHtml(List<string> list, List<CurrencyIdValuePair> currencies)
        {
            var listCurrencies = new List<CurrencyResult>();
            CurrencyResult? curr = null;

            foreach (var item in list)
            {
                if (currencies.Select(x => x.Code).Contains(item))
                {
                    if (curr?.Currency != item)
                    {
                        if (curr is not null)
                            listCurrencies.Add(curr);

                        curr = new CurrencyResult
                        {
                            Currency = item,
                            CurrencyId = currencies.First(x => x.Code == item).Id,
                            Rates = new List<decimal>()
                        };
                    }
                }
                else if (curr is not null && decimal.TryParse(item, out decimal number) && curr.Rates.Count < 2)
                {
                    curr.Rates.Add(decimal.Parse(item));
                }
            }

            if (curr is not null)
                listCurrencies.Add(curr);

            return listCurrencies;
        }

        private static List<Rate> CunstructRateEntityModel(List<CurrencyResult> listCurrencies, int bankId)
        {
            var rates = new List<Rate>();

            foreach (var item in listCurrencies)
            {
                rates.Add(new Rate
                {
                    BankId = bankId,
                    CurrencyId = item.CurrencyId,
                    Buy = item.Rates.Any() ? item.Rates.Min() : null,
                    Sell = item.Rates.Any() ? item.Rates.Max() : null
                });
            }

            return rates;
        }
        #endregion

    }
}
