using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace apiClient
{
    public class Client
    {
        private HttpClient client;
        public Client()
        {
            HttpClientHandler handler = new HttpClientHandler();
            handler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) =>
            {
                return true;
            };
            client = new HttpClient(handler);
        }

        public void SetToken(string jwt)
        {
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", jwt);
        }

        private async Task<string> ResponseProcessing(HttpResponseMessage response)
        {
            response.EnsureSuccessStatusCode();
            var jsonString = await response.Content.ReadAsStringAsync();
            return jsonString;
        }

        public async Task<string> Post(string url, object data)
        {
            var result = await client.PostAsync(GetUrl(url), SerializeContentToJson(data));
            return await ResponseProcessing(result);
        }

        public async Task<string> Put(string url, object data)
        {
            var result = await client.PutAsync(GetUrl(url), SerializeContentToJson(data));
            return await ResponseProcessing(result);
        }

        public async Task<string> Delete(string url)
        {
            var result = await client.DeleteAsync(GetUrl(url));
            return await ResponseProcessing(result);
        }

        public async Task<string> Get(string url)
        {
            var result = await client.GetAsync(GetUrl(url));
            return await ResponseProcessing(result);
        }

        #region  private functions
        private string GetUrl(string url)
        {
            return "https://localhost:5001/" + url;
            // return "https://lcbo.tma.com.vn/" + url;
        }

        private StringContent SerializeContentToJson(object data)
        {
            var json = JsonConvert.SerializeObject(data);
            var result = new StringContent(
               json, Encoding.UTF8, "application/json");
            return result;
        }
        #endregion


    }
}