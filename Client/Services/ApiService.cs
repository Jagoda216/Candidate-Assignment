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
    /// <summary>
    /// Provides methods for interacting with the API
    /// </summary>
    public class ApiService
    {
        private readonly HttpClient _httpClient;
		private readonly Authentication _authentication;
		
		/// <summary>
		/// Initializes a new instance of authentication and configures HTTP client
		/// </summary>
        public ApiService()
        {

			_httpClient = new HttpClient
            {
                BaseAddress = new Uri("https://localhost:5001")
            };

			_authentication = new Authentication(_httpClient);

        }

        /// <summary>
        /// Authenticates a user by sending login credentials to the API.
        /// </summary>
        /// <param name="username">The user's username</param>
        /// <param name="password">The user's password</param>
        /// <returns>A JWT session token if authentication is successful</returns>

        public async Task<string> LoginAsync(string username, string password)
        {
            //Creates a new object for json structure 
            var loginData = new { Name = username, Password = password };
            //Converts loginData  into a json formatted string
            var json = JsonConvert.SerializeObject(loginData);
            //Wraps json string into a StringContent object for http request
            var content = new StringContent(json, Encoding.UTF8, "application/json");

			try
            {
                //Sends an http post request to specified API endpoint with content
                var response = await _httpClient.PostAsync("api/login/login", content);
				if(response.IsSuccessStatusCode)
				{
                    //Reads the response content as a string
                    var result = await response.Content.ReadAsStringAsync();
                    //Deserializes the JSON response to get token
                    var token = JsonConvert.DeserializeObject<dynamic>(result)?.token.ToString();

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

        /// <summary>
        /// Updates the user's quantity in the database
        /// </summary>
        /// <param name="token">The session token for authentication</param>
        /// <param name="username">The user's username</param>
        /// <param name="quantity">The quantity to add or update in the database</param>
        /// <returns>Returns status if the update was successful or not</returns>
        public async Task<bool> SubmitQuanityAysnc(string token, string username, int quantity)
		{
			//Creates a new object for json structure 
			var submitData = new { Name = username, Quantity = quantity };
            //Converts submitData  into a json formatted string
            var json = JsonConvert.SerializeObject(submitData);
			//Wraps json string into a StringContent object for http request
			var content = new StringContent(json, Encoding.UTF8, "application/json");
			//Calls SetToken function for setting http header with session token
			_authentication.SetToken(token);

			try
			{
                //Sends an http post request to specified API endpoint with content
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
