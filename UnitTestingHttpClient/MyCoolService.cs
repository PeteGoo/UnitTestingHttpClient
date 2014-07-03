using System.Net.Http;
using System.Threading.Tasks;

namespace UnitTestingHttpClient
{
    public class MyCoolService
    {

        private readonly HttpMessageHandler _messageHandler;

        public MyCoolService(HttpMessageHandler messageHandler)
        {
            _messageHandler = messageHandler;
        }

        public MyCoolService() : this(new HttpClientHandler())
        {
        }

        public async Task<string> GetGoogleContent()
        {
            using (var httpClient = new HttpClient(_messageHandler))
            {
                var response = await httpClient.GetAsync("http://www.google.com/");

                response.EnsureSuccessStatusCode();

                return await response.Content.ReadAsStringAsync();
            }
        }
    }
}