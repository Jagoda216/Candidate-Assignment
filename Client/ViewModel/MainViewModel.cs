using Client.Command;
using Client.Model;
using Client.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Client.ViewModel
{
    /// <summary>
    /// ViewModel for submit data
    /// </summary>
    public class MainViewModel
    {
        private readonly User _user;
        private readonly ApiService _apiService;

        private string _token;

        /// <summary>
        /// Command bound to the Submit button
        /// </summary>
        public ICommand SubmitCommand { get; }

        /// <summary>
        /// Gets or sets user's quantity
        /// </summary>
        public int Quantity {  get => _user.Quantity ; 
            set 
            {
                if( _user.Quantity != value )
                {
                    _user.Quantity = value;
				}
            }
        }
        /// <summary>
        /// Creating a new instance of MainViewModel
        /// </summary>
        /// <param name="username">User's username</param>
        /// <param name="token">User's session token</param>
        public MainViewModel(string username, string token)
        {
            _user = new User();
            _apiService = new ApiService();
            _token = token;

            _user.Username = username;

            SubmitCommand = new RelayCommand(Submit, CanSubmit);
        }
        /// <summary>
        /// Submit new quantity to the server
        /// </summary>
        public async void Submit()
        {
            var status = await _apiService.SubmitQuanityAysnc(_token, _user.Username, _user.Quantity);
            if (status)
            {
                MessageBox.Show("Quantity was successfully added.");
            }
            else
            {
                MessageBox.Show("Error occurred. Quantity was not added.");
            }

        }

        public bool CanSubmit()
        {
            return true;
        }

	}
}
