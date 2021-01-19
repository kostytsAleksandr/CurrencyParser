using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using CurrencyParserConsole.Entities;
using CurrencyParserConsole.Services;
using HtmlAgilityPack;

namespace CurrencyParserConsole
{
    class Program
    {
        private static readonly HttpClient client = new HttpClient();
        static void Main(string[] args)
        {
            Run().GetAwaiter().GetResult();
        }

        private static async Task Run()
        {
            var document = new HtmlDocument();
            try
            {
                string responseBody = await client.GetStringAsync("https://minfin.com.ua/");
                document.LoadHtml(responseBody);
                var minfinData = document.DocumentNode.
                    SelectNodes("/html/body/main/div/div/div[1]/div/div[1]/div[2]/a[1]/span[2]/span[1]/text()").
                    FirstOrDefault();
                double minfinCourseUSDBuying;
                double.TryParse(minfinData.InnerHtml.Replace(",", "."), out minfinCourseUSDBuying);
                string mono = await client.GetStringAsync("https://api.monobank.com.ua/bank/currency");
                JsonStateConverter jsonStateConverter = new JsonStateConverter();
                List<Currency> monoCurrencies = jsonStateConverter.FromRange<Currency>(mono);
                Console.WriteLine($"minfin Course Buying USD: {minfinCourseUSDBuying}  mono Sale USD: {monoCurrencies.FirstOrDefault(f => f.CurrencyCodeA == 840).RateSell}");
                if (minfinCourseUSDBuying > monoCurrencies.FirstOrDefault(f => f.CurrencyCodeA == 840).RateSell)
                {
                    Console.WriteLine($"minfin Course Buying USD: {minfinCourseUSDBuying}  mono Sale USD: {monoCurrencies.FirstOrDefault(f => f.CurrencyCodeA == 840).RateSell}");
                    Console.WriteLine("You can get benefit");
                }
            }

            catch (HttpRequestException e)
            {
                Console.WriteLine("\nException Caught!");
                Console.WriteLine("Message :{0} ", e.Message);
            }
        }
    }
}
