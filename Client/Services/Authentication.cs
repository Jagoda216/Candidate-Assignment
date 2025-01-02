using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Client.Services
{
    public class Authentication
    {
        private readonly HttpClient _httpClient;

        private string _token;

        public Authentication(HttpClient httpClient) 
        {
            _httpClient = httpClient;
        }

        public void SetToken(string token)
        {
            _token = token;
            _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
        }

    }
}
