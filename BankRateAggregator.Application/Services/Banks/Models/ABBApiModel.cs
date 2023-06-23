using BankRateAggregator.Application.Services.Currency.Models;
using BankRateAggregator.Domain.Entities.BankRates;
using System.Xml.Serialization;

namespace BankRateAggregator.Application.Services.Banks.Models
{

    [XmlRoot(ElementName = "Response")]
    public class ABBApiModel : BaseApiXMLModel
    {

        [XmlElement(ElementName = "Rates")]
        public Rates Rates { get; set; }

        public override List<Rate>? DeserializeObject(Stream responseStream, List<CurrencyIdValuePair> currencies, int bankId)
        {
            List<Rate>? rates = new();

            XmlSerializer serializer = new(typeof(ABBApiModel));
            ABBApiModel model = (ABBApiModel)serializer.Deserialize(responseStream);

            rates.AddRange(model.Rates.Rate.Select(item => new Rate
            {
                Buy = Convert.ToDecimal(item.Value1),
                Sell = Convert.ToDecimal(item.Value2),
                CurrencyId = currencies.First(x => x.Code == item.Cur).Id,
                BankId = bankId
            }));
            return rates;
        }
    }

    [XmlRoot(ElementName = "rate")]
    public class ABBRate
    {

        [XmlAttribute(AttributeName = "cur")]
        public string Cur { get; set; }

        [XmlAttribute(AttributeName = "value1")]
        public double Value1 { get; set; }

        [XmlAttribute(AttributeName = "value2")]
        public double Value2 { get; set; }

    }

    [XmlRoot(ElementName = "Rates")]
    public class Rates
    {

        [XmlElement(ElementName = "rate")]
        public List<ABBRate> Rate { get; set; }
    }

}
