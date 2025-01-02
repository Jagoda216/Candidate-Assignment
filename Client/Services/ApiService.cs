using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Client.Services;

namespace Client.Services
{
    public class ApiService
    {
        private readonly HttpClient _httpClient;
		private readonly Authentication _authentication;
		
        public ApiService()
        {

			_httpClient = new HttpClient
            {
                BaseAddress = new Uri("https://localhost:5001")
            };

			_authentication = new Authentication(_httpClient);

        }
		
		public async Task<string> LoginAsync(string username, string password)
        {

            var loginData = new { Name = username, Password = password };
            var json = JsonConvert.SerializeObject(loginData);
			var content = new StringContent(json, Encoding.UTF8, "application/json");

			try
            {
                var response = await _httpClient.PostAsync("api/login/login", content);
				if(response.IsSuccessStatusCode)
				{
					var result = await response.Content.ReadAsStringAsync();
					var token = JsonConvert.DeserializeObject<dynamic>(result)?.token.ToString();
					
					_authentication.SetToken(token);

					return token;
				}

                return "";
            }
            catch (Exception ex)
			{
				Console.WriteLine($"Error connecting to server: {ex.Message}");	
				
				return "";

			}
        }

		public async Task<bool> SubmitQuanityAysnc(string token, string username, int quantity)
		{
			var submitData = new { Name = username, Quantity = quantity };
			var json = JsonConvert.SerializeObject(submitData);
			var content = new StringContent(json, Encoding.UTF8, "application/json");

			_authentication.SetToken(token);

			try
			{
				var response = await _httpClient.PostAsync("api/updateDataBase/updateQuantity", content);

				Console.WriteLine($"Response: {response.StatusCode}");

                return response.IsSuccessStatusCode;
			}
			catch (Exception ex)
			{
				Console.WriteLine($"Error: {ex.Message}");

				return false;
			}
		}
	}
}
