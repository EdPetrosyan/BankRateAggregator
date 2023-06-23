namespace BankRateAggregator.Application.Services.Banks.Models
{
    public static class BaseApiXMLFactory
    {
        public static BaseApiXMLModel GetApiModel(string url)
        {
            var factory = _factories[url];
            return factory();
        }

        private static readonly Dictionary<string, Func<BaseApiXMLModel>> _factories =
                      new()
                      {
                    { "https://www.armbusinessbank.am/rates/Rates991.xml", ()=>new ABBApiModel() }
                  };
    }
}
