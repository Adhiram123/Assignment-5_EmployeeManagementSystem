using System.Text;

namespace EmployeeManagementSystem.Common
{
    public class HttpClientHelper
    {
        //make PostRequest
        //make GEtREquest

        public static async Task<string> MakePostRequest(string baseUrl, string endpoint, string apiRequestData)
        {
            var socketsHandler = new SocketsHttpHandler
            {
                PooledConnectionLifetime = TimeSpan.FromMinutes(10),
                PooledConnectionIdleTimeout = TimeSpan.FromMinutes(10),
                MaxConnectionsPerServer = 10


            };
            using (HttpClient httpClient = new HttpClient(socketsHandler))
            {

                httpClient.Timeout = TimeSpan.FromMinutes(5);
                httpClient.BaseAddress = new Uri(baseUrl);
                StringContent apiRequestContent = new StringContent(apiRequestData, Encoding.UTF8, "application/json");

                var httpResponse = httpClient.PostAsync(endpoint, apiRequestContent).Result;

                var httpResponseString = httpResponse.Content.ReadAsStringAsync().Result;

                if (!httpResponse.IsSuccessStatusCode)
                {
                    throw new Exception(httpResponseString);
                }
                return httpResponseString;
            }



        }
        public static async Task<string> MakeGetRequest(string baseUrl, string endpoint)
        {
            var socketsHandler = new SocketsHttpHandler
            {
                PooledConnectionLifetime = TimeSpan.FromMinutes(10),
                PooledConnectionIdleTimeout = TimeSpan.FromMinutes(5),
                MaxConnectionsPerServer = 10


            };
            using (HttpClient httpClient = new HttpClient(socketsHandler))
            {

                httpClient.Timeout = TimeSpan.FromMinutes(5);
                httpClient.BaseAddress = new Uri(baseUrl);

                var response = await httpClient.GetAsync(endpoint);

                var responseString = await response.Content.ReadAsStringAsync();
                if (!response.IsSuccessStatusCode)
                {
                    throw new Exception(responseString);
                }
                return responseString;
            }

        }
    }
}
