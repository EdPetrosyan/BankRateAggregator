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

        /// <summary>
        /// Web Scrapping directly from Bank Web Page to get today's rates
        /// </summary>
        /// <param name="url">Bank WebPage WUL</param>
        /// <param name="xPath">XPath for specific form that stores rates</param>
        /// <param name="bankId">Bank Id</param>
        /// <param name="currencies">Currency List that we should parse</param>
        /// <param name="cancellationToken">cancellation Token for cancelling Task</param>
        /// <returns>Parsed Rates Data From WebPage URL</returns>
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

                //getting html result from website URL and load it into HTML document
                var html = await response.Content.ReadAsStringAsync(cancellationToken);
                var htmlDocument = new HtmlDocument();
                htmlDocument.LoadHtml(html);

                // from xPath we'll get the specific form that rates are stored
                var heading = htmlDocument.DocumentNode.SelectNodes(xpath: xPath).FirstOrDefault();
                if (heading != null)
                {
                    // ignoring additional characters to have only list of items that we will consider
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

        /// <summary>
        /// Make Request To Bank API with JSON result and Deserializing the Result
        /// </summary>
        /// <param name="model">classes that derived from BaseApiModel</param>
        /// <param name="url">Bank Api URL</param>
        /// <param name="bankId">Bank Id</param>
        /// <param name="currencies">Currency List that we should parse</param>
        /// <param name="cancellationToken">cancellation Token for cancelling Task</param>
        /// <returns>Parsed Rates Data From JSON API Call</returns>
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

                // calling Deserialize from inherited class with its specific deserialization for JSON based APIs
                return model.DeserializeObject(responseBody, currencies, bankId);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred during ApiCallJson");
            }
            return await Task.FromResult<List<Rate>?>(null);
        }

        /// <summary>
        /// Make Request To Bank API with XML result and Deserializing the Result
        /// </summary>
        /// <param name="model">classes that derived from BaseApiXMLModel</param>
        /// <param name="url">Bank Api URL</param>
        /// <param name="bankId">Bank Id</param>
        /// <param name="currencies">Currency List that we should parse</param>
        /// <param name="cancellationToken">cancellation Token for cancelling Task</param>
        /// <returns>Parsed Rates Data From XML API Call</returns>
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

                // calling Deserialize from inherited class with its specific deserialization for XML based APIs
                return model.DeserializeObject(responseStream, currencies, bankId);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred during ApiCallXml");
            }
            return await Task.FromResult<List<Rate>?>(null); ;
        }


        /// <summary>
        /// Getting Currencies with its Aliases 
        /// </summary>
        /// <param name="cancellationToken">cancellation token for cancelling Task</param>
        /// <returns>Currencies With Aliases</returns>
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

            // iteration from HTML result list that we've get from Web Scrapping
            foreach (var item in list)
            {
                // if Currency from Bank is the currency we want to parse
                if (currencies.Select(x => x.Code).Contains(item))
                {
                    // it's a new Currency
                    if (curr?.Currency != item)
                    {
                        //Adding already existing currency to the list (if exists) 
                        if (curr is not null)
                            listCurrencies.Add(curr);

                        //init new currency
                        curr = new CurrencyResult
                        {
                            Currency = item,
                            CurrencyId = currencies.First(x => x.Code == item).Id,
                            Rates = new List<decimal>()
                        };
                    }
                }
                // When thare is a rate number we get that and ignoring the results where central bank rates are exists
                else if (curr is not null && decimal.TryParse(item, out decimal number) && curr.Rates.Count < 2)
                {
                    curr.Rates.Add(decimal.Parse(item));
                }
            }

            //for last iteration if there are any currency left
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
                    Buy = item.Rates.Any() ? item.Rates.Min() : null, // minimum value for BUY rate
                    Sell = item.Rates.Any() ? item.Rates.Max() : null // maximum Value for SELL rate
                });
            }

            return rates;
        }
        #endregion

    }
}
