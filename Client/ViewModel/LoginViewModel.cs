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
    public class LoginViewModel
	{
        private readonly User _user;
        private readonly ApiService _apiService;
        private readonly Navigation _navigation;

        public ICommand LoginCommand { get; }
        
        public string Username { get => _user.Username; 
            set
            {
                if (_user.Username != value)
                {
                    _user.Username = value;  
                }
            }
        }
        public string Password { get => _user.Password; 
            set 
            {
                if (_user.Password != value)  
                {
                    _user.Password = value;
                }
            } 
        }
        public LoginViewModel( )
        {
            _user = new User();
            _apiService = new ApiService();
            _navigation = new Navigation();

            LoginCommand = new RelayCommand(Login, CanLogin);
            
        }

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

        public bool CanLogin()
        {
            return !string.IsNullOrEmpty(_user.Username) && !string.IsNullOrEmpty(_user.Password);
        }
	}
}
