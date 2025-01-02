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
    public class MainViewModel
    {
        private readonly User _user;
        private readonly ApiService _apiService;

        private string _token;

        public ICommand SubmitCommand { get; }

        public int Quantity {  get => _user.Quantity ; 
            set 
            {
                if( _user.Quantity != value )
                {
                    _user.Quantity = value;
				}
            }
        }
        
        public MainViewModel(string username, string token)
        {
            _user = new User();
            _apiService = new ApiService();
            _token = token;

            _user.Username = username;

            SubmitCommand = new RelayCommand(Submit, CanSubmit);
        }

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
