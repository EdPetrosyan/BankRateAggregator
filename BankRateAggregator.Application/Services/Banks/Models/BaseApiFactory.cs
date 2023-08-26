namespace BankRateAggregator.Application.Services.Banks.Models
{
    public static class BaseApiFactory
    {
        public static BaseApiModel GetApiModel(string url)
        {
            var factory = _factories[url];
            return factory();
        }

        private static readonly Dictionary<string, Func<BaseApiModel>> _factories =
                      new()
                      {
                    { "https://www.armswissbank.am/include/ajax.php", ()=>new ArmSwissBankApiModel() },
                    { "https://sapi.conversebank.am/api/v2/currencyrates", ()=>new ConverseBankApiModel() },
                    { "https://mobileapi.fcc.am/FCBank.Mobile.Api_V2/api/publicInfo/getRates?langID=2", ()=>new FastBankApiModel() },
                    { "https://www.inecobank.am/api/rates/", ()=>new InecoBankApiModel() },
                    { "https://api.mellatbank.am/api/v1/rate/list", ()=>new MellatBankApiModel() }
                  };
    }
}
