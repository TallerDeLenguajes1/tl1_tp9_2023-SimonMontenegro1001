using System.Text.Json;

namespace tp9
{
    internal class Program
    {
        static readonly HttpClient client = new HttpClient();

        static async Task Main()
        {
            try
            {
                using HttpResponseMessage response = await client.GetAsync("https://api.coindesk.com/v1/bpi/currentprice.json");
                response.EnsureSuccessStatusCode();
                using (Stream strReader = await response.Content.ReadAsStreamAsync())
                {
                    if (strReader == null) return;
                    using (StreamReader objReader = new StreamReader(strReader))
                    {
                        string responseBody = objReader.ReadToEnd();
                        IntercambioBtc intercambioBtc = JsonSerializer.Deserialize<IntercambioBtc>(responseBody);
                        System.Console.WriteLine("Intercambio actual:");
                        System.Console.WriteLine(intercambioBtc.bpi.EUR.description + ": $" + intercambioBtc.bpi.EUR.rate);
                        System.Console.WriteLine(intercambioBtc.bpi.GBP.description + ": $" + intercambioBtc.bpi.GBP.rate);
                        System.Console.WriteLine(intercambioBtc.bpi.USD.description + ": $" + intercambioBtc.bpi.USD.rate);
                        System.Console.WriteLine($"Caracteristicas de intercambio con {intercambioBtc.bpi.EUR.description}: ");
                        System.Console.WriteLine($"propiedad code: " + intercambioBtc.bpi.USD.code);
                        System.Console.WriteLine($"propiedad symbol: " + intercambioBtc.bpi.USD.symbol);
                        System.Console.WriteLine($"propiedad rate: " + intercambioBtc.bpi.USD.rate);
                        System.Console.WriteLine($"propiedad description: " + intercambioBtc.bpi.USD.description);
                        System.Console.WriteLine($"propiedad rate_float: " + intercambioBtc.bpi.USD.rate_float);
                    }
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