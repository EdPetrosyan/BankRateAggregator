using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BankRateAggregator.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Seed_bank_data : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                                    INSERT INTO public.""Banks"" (""Name"", ""WebSiteUrl"", ""Created"", ""CreatedBy"", ""LastModified"", ""LastModifiedBy"",""RateApiUrl"",""RateXPath"")
                                    VALUES
                                        ('Acba Bank', 'https://www.acba.am', CURRENT_DATE, 'script', CURRENT_DATE, 'script',null,'/html/body/div[2]/div[17]/div[2]/div[2]/div/div[4]/div[1]'),
                                        ('AraratBank', 'https://www.araratbank.am/hy/', CURRENT_DATE, 'script', CURRENT_DATE, 'script',null,'/html/body/main/div[2]/div/div/div[3]/div/div/div/div[2]'),
                                        ('Unibank', 'https://www.unibank.am', CURRENT_DATE, 'script', CURRENT_DATE, 'script',null,'/html/body/div/div[2]/section[1]/div/div/div/div[2]/div[1]/div[1]/ul[2]'),
                                        ('Fast Bank', 'https://www.fastbank.am', CURRENT_DATE, 'script', CURRENT_DATE, 'script', 'https://mobileapi.fcc.am/FCBank.Mobile.Api_V2/api/publicInfo/getRates?langID=2',null),
                                        ('Artsakhbank', 'http://www.artsakhbank.am', CURRENT_DATE, 'script', CURRENT_DATE, 'script',null,'/html/body/div[2]/div[3]/div/div[2]/div[2]/div[2]/div[1]/ul[2]'),
                                        ('VTB Bank', 'https://www.vtb.am/am/currency', CURRENT_DATE, 'script', CURRENT_DATE, 'script',null,'/html[1]/body[1]/div[1]/div[2]/div[3]/div[2]/main[1]/div[1]/section[5]/div[1]/div[1]/div[1]/div[2]/div[1]/div[1]/div[1]/section[1]/div[2]/div[1]/div[1]/div[2]/div[1]/div[2]/table[1]/tbody[1]'),
                                        ('Evocabank', 'https://www.evoca.am', CURRENT_DATE, 'script', CURRENT_DATE, 'script',null,'/html/body/main/div[9]/div/div[1]/div/div[1]/div/div[1]/div/div[1]/div/div/table/tbody'),
                                        ('Inecobank', 'http://www.inecobank.am', CURRENT_DATE, 'script', CURRENT_DATE, 'script','https://www.inecobank.am/api/rates/',null),
                                        ('IDBank', 'https://idbank.am/rates/', CURRENT_DATE, 'script', CURRENT_DATE, 'script',null,'/html/body/main/div[2]/div/div/div'),
                                        ('Byblos Bank', 'https://www.byblosbankarmenia.am', CURRENT_DATE, 'script', CURRENT_DATE, 'script',null,'/html/body/main/div/div[4]/div/div[2]/div/div[2]/div[2]/div[2]/table'),
                                        ('ArmSwissBank', 'https://www.armswissbank.am', CURRENT_DATE, 'script', CURRENT_DATE, 'script','https://www.armswissbank.am/include/ajax.php',null),
                                        ('Ardshinbank', 'https://www.ardshinbank.am', CURRENT_DATE, 'script', CURRENT_DATE, 'script',null,'/html/body/div[10]/div/div/div[3]/div[2]/div[1]/table'),
                                        ('ArmBusinessBank', 'https://www.armbusinessbank.am/', CURRENT_DATE, 'script', CURRENT_DATE, 'script','https://www.armbusinessbank.am/rates/Rates991.xml',null),
                                        ('Converse Bank', 'https://www.conversebank.am', CURRENT_DATE, 'script', CURRENT_DATE, 'script','https://webapi.conversebank.am/api/v2/currencyrates',null),
                                        ('Mellat Bank', 'https://www.mellatbank.am', CURRENT_DATE, 'script', CURRENT_DATE, 'script','https://api.mellatbank.am/api/v1/rate/list',null),
                                        ('ARMECONOMBANK', 'https://www.aeb.am', CURRENT_DATE, 'script', CURRENT_DATE, 'script',null,'/html/body/main/div[5]/div/div[3]/div/section[1]/table/tbody');
                                    ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
