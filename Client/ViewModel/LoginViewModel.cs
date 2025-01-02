using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Client.Model;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Windows.Input;
using Client.Command;
using Client.Services;
using Client.View;
using System.Windows;

namespace Client.ViewModel
{
    /// <summary>
    /// ViewModel for managing login functionality and navigation
    /// </summary>
    public class LoginViewModel
	{
        private readonly User _user;
        private readonly ApiService _apiService;
        private readonly Navigation _navigation;
        /// <summary>
        /// Command bound to the Login button
        /// </summary>
        public ICommand LoginCommand { get; }
        /// <summary>
        /// Gets or sets user's username from View
        /// </summary>
        public string Username { get => _user.Username; 
            set
            {
                if (_user.Username != value)
                {
                    _user.Username = value;  
                }
            }
        }
        /// <summary>
        /// Gets or sets user's password from View
        /// </summary>
        public string Password { get => _user.Password; 
            set 
            {
                if (_user.Password != value)  
                {
                    _user.Password = value;
                }
            } 
        }
        /// <summary>
        /// Initializes a new instance LoginViewModel
        /// </summary>
        public LoginViewModel( )
        {
            _user = new User();
            _apiService = new ApiService();
            _navigation = new Navigation();

            LoginCommand = new RelayCommand(Login, CanLogin);
            
        }
        /// <summary>
        /// Attemps to log in using user's username and password
        /// If successful navigate to MainView
        /// </summary>
        public async void Login()
        {
            string token = await _apiService.LoginAsync(_user.Username, _user.Password);

			if (!string.IsNullOrEmpty(token))
            {
                _navigation.Navigate<MainView>(_user.Username, token);
            }
            else
            {
                MessageBox.Show("Invalid username or password. Please try again.");
            }
        }
        /// <summary>
        /// Determines of the login action can be excuted
        /// </summary>
        /// <returns>True if username and passwrods are provided</returns>
        public bool CanLogin()
        {
            return !string.IsNullOrEmpty(_user.Username) && !string.IsNullOrEmpty(_user.Password);
        }
	}
}
