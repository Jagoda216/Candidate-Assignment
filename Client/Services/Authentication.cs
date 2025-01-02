using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Client.Services
{
    /// <summary>
    /// Provides methods for handling authentication and for setting authorization token
    /// </summary>
    public class Authentication
    {
        private readonly HttpClient _httpClient;

        private string _token;
        /// <summary>
        /// Initializes a new instance Authentication
        /// </summary>
        /// <param name="httpClient">Used for making API requests </param>
        public Authentication(HttpClient httpClient) 
        {
            _httpClient = httpClient;
        }
        /// <summary>
        /// Setting HTTP header for authentication
        /// </summary>
        /// <param name="token">User's session token</param>
        public void SetToken(string token)
        {
            _token = token;

            //Sets HTTP header with a JWT session token and Bearer
            _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
        }

    }
}
