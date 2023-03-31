using System.Text.RegularExpressions;
using System.Net.Http;
internal class Program
{
    async public static Task Main(string[] args)
    {

        if (args.Length == 0) {
            throw new ArgumentNullException();
            return;
        }
        try
        {
            Uri url = new Uri(args[0]);
            HttpClient client = new HttpClient();

            HttpResponseMessage response = await client.GetAsync(url);
            response.EnsureSuccessStatusCode();
            string html = await response.Content.ReadAsStringAsync();
            string emailRegex = @"[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}";
            MatchCollection matches = Regex.Matches(html, emailRegex);
            if(matches.Count <= 0 ) {
                throw new Exception("Nie znaleziono adresów email");
                return;
            }
            foreach (Match match in matches)
            {
                Console.WriteLine(match.Value);
            }
        }
        catch (HttpRequestException e)
        {
            Console.WriteLine("Błąd w czasie pobierania strony", e.Message);
        }
        catch (UriFormatException ex) {
            Console.WriteLine("bledny adres url", ex.Message);
        }



    }
}
